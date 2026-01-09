using Asp.Versioning.Builder;
using RestaurantManagement.Reservation.Api.Features.Reservations.Create;
using RestaurantManagement.Reservation.Api.Features.Reservations.GetAll;


namespace RestaurantManagement.Reservation.Api.Features.Reservations
{
    public static class ReservationEndpointExt
    {
        public static void AddReservationGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/reservations").WithTags("Reservations").WithApiVersionSet(apiVersionSet)
                .CreateReservationGroupItemEndpoint()
                .GetAllReservationGroupItemEndpoint();
        }
    }
}
