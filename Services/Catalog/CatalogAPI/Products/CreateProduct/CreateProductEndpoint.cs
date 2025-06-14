// Khai báo namespace cho tính năng CreateProduct thuộc dịch vụ Catalog.API
namespace Catalog.API.Products.CreateProduct;

// Định nghĩa DTO (Data Transfer Object) để nhận dữ liệu từ client khi tạo sản phẩm
public record CreateProductRequest(
    string Name,                 // Tên sản phẩm
    List<string> Category,      // Danh sách danh mục của sản phẩm
    string Description,         // Mô tả sản phẩm
    string ImageFile,           // Tên hoặc đường dẫn file ảnh
    decimal Price               // Giá sản phẩm
);

// Định nghĩa DTO để trả dữ liệu lại cho client sau khi tạo sản phẩm thành công
public record CreateProductResponse(Guid Id); // ID sản phẩm mới được tạo

// Định nghĩa endpoint xử lý route /products bằng Carter (không cần controller)
public class CreateProductEndpoint : ICarterModule
{
    // Phương thức bắt buộc khi implement ICarterModule để đăng ký các routes
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Đăng ký route POST /products
        app.MapPost("/products",

            // Hàm lambda xử lý request khi người dùng gọi POST /products
            async (CreateProductRequest request, ISender sender) =>
            {
                // Chuyển đổi request từ DTO sang Command thông qua Mapster
                var command = request.Adapt<CreateProductCommand>();

                // Gửi command đến MediatR để gọi handler tương ứng xử lý tạo sản phẩm
                var result = await sender.Send(command);

                // Map kết quả từ handler sang DTO để trả về client
                var response = result.Adapt<CreateProductResponse>();

                // Trả về HTTP 201 Created kèm theo body là thông tin sản phẩm mới
                return Results.Created($"/products/{response.Id}", response); 
            })

            // Đặt tên cho endpoint này là "CreateProduct" (dùng được cho Swagger)
            .WithName("CreateProduct")

            // Khai báo kiểu trả về khi thành công: HTTP 201 và response dạng CreateProductResponse
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)

            // Khai báo kiểu trả về khi lỗi: HTTP 400 BadRequest
            .ProducesProblem(StatusCodes.Status400BadRequest)

            // Tóm tắt ý nghĩa endpoint (hiển thị trên Swagger)
            .WithSummary("Create Product")

            // Mô tả chi tiết cho endpoint (hiển thị trên Swagger)
            .WithDescription("Create Product");
    }
}
