using Asp.Versioning.Builder;
using RestaurantManagement.Menu.Api.Features.Menus.Create;
using RestaurantManagement.Menu.Api.Features.Menus.GetAll;
using RestaurantManagement.Menu.Api.Features.Menus.GetById;

namespace RestaurantManagement.Menu.Api.Features.Menus
{
    public static class MenuEndpointExt
    {
        public static void AddMenuGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/menus").WithTags("Menus")
                .WithApiVersionSet(apiVersionSet)
                .CreateMenuGroupItemEndpoint()
                .GetAllMenuGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint();
        }
    }
}
