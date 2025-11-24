using Asp.Versioning.Builder;
using RestaurantManagement.Menu.Api.Features.Products.Create;
using RestaurantManagement.Menu.Api.Features.Products.Delete;
using RestaurantManagement.Menu.Api.Features.Products.GetAll;
using RestaurantManagement.Menu.Api.Features.Products.GetAllByUserId;
using RestaurantManagement.Menu.Api.Features.Products.GetById;
using RestaurantManagement.Menu.Api.Features.Products.Update;

namespace RestaurantManagement.Menu.Api.Features.Products
{
    public static class ProductEndpointExt
    {
        public static void AddProductGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/products").WithTags("Product").WithApiVersionSet(apiVersionSet)
                .GetAllProductGroupItemEndpoint()
                .CreateProductGroupItemEndpoint()
                .GetByIdProductGroupItemEndpoint()
                .UpdateProductGroupItemEndpoint()
                .DeleteProductGroupItemEndpoint()
                .GetByUserIdProductGroupItemEndpoint();
        }
    }
}
