
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket.DeleteBasket;

//public record DeleteBasketRequest(string UserName);

public record DeleteBasketResponse(bool ISuccess);

public class DeleteBasketEndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            // Gửi lệnh xóa giỏ hàng đến MediatR
            var result = await sender.Send(new DeleteBasketCommand(userName));
            // Chuyển đổi kết quả thành DTO để trả về
            var response = result.Adapt<DeleteBasketResponse>();
            // Trả về kết quả dưới dạng HTTP 200 OK
            return Results.Ok(response);
        })
            .WithName("DeleteProduct")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
    }
}
