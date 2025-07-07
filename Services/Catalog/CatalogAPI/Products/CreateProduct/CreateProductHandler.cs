// Namespace chứa slice CreateProduct của Catalog.API
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Catalog.API.Products.CreateProduct;

// Định nghĩa Command được gửi từ handler (qua MediatR)
// Đây là lệnh tạo sản phẩm mới, chứa thông tin mà client gửi lên
public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<CreateProductResult>;  // Kế thừa interface ICommand trả về CreateProductResult

// Định nghĩa kết quả sau khi tạo sản phẩm thành công — chỉ chứa Id của sản phẩm mới
public record CreateProductResult(Guid Id);

//

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required!");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Product category is required!");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Product imageFile is required!");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product Price must be greater than 0!");
    }
}

// Handler xử lý Command CreateProductCommand và trả về CreateProductResult
internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    // Phương thức chính để xử lý command được gửi đến
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
  


        // Tạo đối tượng Product mới từ thông tin trong command
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        // 🔧 TODO: Sau này nên thêm logic lưu vào database ở đây
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // Trả về kết quả với Id mới sinh ngẫu nhiên (tạm thời, chưa lưu vào DB)
        return new CreateProductResult(Guid.NewGuid());
    }
}
