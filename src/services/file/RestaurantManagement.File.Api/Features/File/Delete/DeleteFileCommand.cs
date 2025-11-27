using RestaurantManagement.Shared;

namespace RestaurantManagement.File.Api.Features.File.Delete
{
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
}
