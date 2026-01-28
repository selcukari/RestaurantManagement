using Asp.Versioning.Builder;
using RestaurantManagement.Reporting.Api.Features.Reporting.GetAll;

namespace RestaurantManagement.Menu.Api.Features.Products
{
    public static class ReporingEndpointExt
    {
        public static void AddReportingGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/reportings").WithTags("Reporting").WithApiVersionSet(apiVersionSet)
                .GetAllReportingGroupItemEndpoint();
        }
    }
}
