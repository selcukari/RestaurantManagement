using Asp.Versioning.Builder;
using RestaurantManagement.Kitchen.Api.Features.Kitchens.GetAll;

namespace RestaurantManagement.Kitchen.Api.Features.Kitchens
{
    public static class KitchenEndpointExt
    {
        public static void AddKitchenGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/kitchens").WithTags("Kitchens")
                .WithApiVersionSet(apiVersionSet)
                .GetAllKitchenGroupItemEndpoint();
        }
    }
}
