// Tạo một WebApplication builder từ đối số dòng lệnh (args)
// Đây là điểm khởi đầu của ứng dụng ASP.NET Core
var builder = WebApplication.CreateBuilder(args);

// -------------------- Đăng ký các dịch vụ (DI Container) --------------------

// Đăng ký Carter — framework giúp viết endpoint gọn hơn mà không cần controller
builder.Services.AddCarter();

// Đăng ký MediatR — dùng để xử lý CQRS (Command/Query)
// Tự động quét các handler trong assembly hiện tại
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

// Cung cấp khả năng tạo tài liệu OpenAPI (Swagger) cho Minimal API
builder.Services.AddEndpointsApiExplorer();

// Đăng ký SwaggerGen để sinh tài liệu Swagger (swagger.json) và giao diện Swagger UI
builder.Services.AddSwaggerGen(options =>
{
    // Cấu hình tài liệu Swagger với thông tin về API
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Catalog API",            // Tiêu đề hiển thị trong Swagger UI
        Version = "v1",                   // Phiên bản tài liệu API
        Description = "API quản lý sản phẩm" // Mô tả hiển thị trong Swagger
    });
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

// -------------------- Xây dựng ứng dụng --------------------

var app = builder.Build();

// -------------------- Cấu hình pipeline xử lý HTTP --------------------

// Map tất cả các endpoint được định nghĩa qua Carter (dùng ICarterModule)
app.MapCarter();

// Kích hoạt middleware Swagger để trả về tài liệu swagger.json
app.UseSwagger();

// Kích hoạt Swagger UI (giao diện người dùng tại /swagger)
app.UseSwaggerUI();

// Chạy ứng dụng — bắt đầu lắng nghe HTTP request
app.Run();
