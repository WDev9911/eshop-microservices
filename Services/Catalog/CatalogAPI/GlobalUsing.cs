global using Carter;
// ✅ Cho phép bạn sử dụng các thành phần từ thư viện Carter (như ICarterModule, MapCarter)
// ➤ Carter giúp định nghĩa endpoint theo kiến trúc Vertical Slice mà không cần controller truyền thống
// ➤ Với global using, bạn không cần phải viết `using Carter;` ở từng file nữa

global using Mapster;
// ✅ Cho phép dùng các extension method như `Adapt<T>()` để map object giữa các lớp DTO, Command, Model
// ➤ Mapster là thư viện mapping thay thế cho AutoMapper, hiệu suất cao, ít cấu hình hơn

global using MediatR;
// ✅ Cho phép bạn dùng interface `ISender`, `IRequest<T>`, `INotification`, `IRequestHandler<T>`, v.v.
// ➤ MediatR dùng để triển khai kiến trúc CQRS (Command & Query Responsibility Segregation)
// ➤ Với global using, bạn có thể viết `sender.Send(...)` hay `IRequest<T>` ở bất kỳ file nào mà không cần import lại
global using Marten;


global using BuildingBlocks.CQRS;
global using Catalog.API.Models;
global using Catalog.API.Exceptions;