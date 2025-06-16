namespace Catalog.API.Products.UpdateProduct;

public record  UpdateProductRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
 );

public record UpdateProductResponse(bool ISuccess);


public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products",
            async (UpdateProductRequest request, ISender sender) =>
            {
               var command  = request.Adapt<UpdateProductCommand>();

               var result = await sender.Send(command);

               var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
            // Đặt tên cho endpoint này là "CreateProduct" (dùng được cho Swagger)
            .WithName("UpdateProduct")

            .WithTags("Catalog.API")

            // Khai báo kiểu trả về khi thành công: HTTP 201 và response dạng CreateProductResponse
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)

            // Khai báo kiểu trả về khi lỗi: HTTP 400 BadRequest
            .ProducesProblem(StatusCodes.Status400BadRequest)

            .ProducesProblem(StatusCodes.Status404NotFound)

            // Tóm tắt ý nghĩa endpoint (hiển thị trên Swagger)
            .WithSummary("Update Product")

            // Mô tả chi tiết cho endpoint (hiển thị trên Swagger)
            .WithDescription("Update Product");

           

    }
}
   
