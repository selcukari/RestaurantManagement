using Asp.Versioning.Builder;
using RestaurantManagement.File.Api.Features.File.Delete;
using RestaurantManagement.File.Api.Features.File.Upload;

namespace RestaurantManagement.File.Api.Features.File
{
    public static class FileEndpointExt
    {
        public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files").WithTags("files").WithApiVersionSet(apiVersionSet)
                .UploadFileGroupItemEndpoint().DeleteFileGroupItemEndpoint()
                .RequireAuthorization();
        }
    }
}
