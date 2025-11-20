using Asp.Versioning.Builder;
using RestaurantManagement.Menu.Api.Features.Products.GetAll;

namespace RestaurantManagement.Menu.Api.Features.Products
{
    public static class ProductEndpointExt
    {
        public static void AddProductGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/products").WithTags("Product").WithApiVersionSet(apiVersionSet)
                .GetAllProductGroupItemEndpoint();
        }
    }
}
