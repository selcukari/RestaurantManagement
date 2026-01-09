using Asp.Versioning.Builder;
using RestaurantManagement.Reservation.Api.Features.Tables.Create;
using RestaurantManagement.Reservation.Api.Features.Tables.GetAll;
using RestaurantManagement.Reservation.Api.Features.Tables.Update;


namespace RestaurantManagement.Reservation.Api.Features.Tables
{
    public static class TableEndpointExt
    {
        public static void AddTableGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/tables").WithTags("Tables").WithApiVersionSet(apiVersionSet)
                .CreateTableGroupItemEndpoint()
                .UpdateTableGroupItemEndpoint()
                .GetAllTableGroupItemEndpoint();
        }
    }
}
