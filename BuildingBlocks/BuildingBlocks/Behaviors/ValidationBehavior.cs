using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
    {
        // ⬅️ Giai đoạn "Before"
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Count > 0)
            {
                throw new ValidationException(failures); // ❌ Nếu sai thì dừng tại đây
            }
        }

        // ⬅️ Giai đoạn gọi "Handler"
        return await next(); // gọi tiếp handler → chính là `Handle` trong handler thật sự
    }
}
