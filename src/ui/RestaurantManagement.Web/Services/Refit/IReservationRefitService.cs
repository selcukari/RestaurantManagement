using Refit;
using RestaurantManagement.Web.Dto;

namespace RestaurantManagement.Web.Services.Refit
{
    public interface IReservationRefitService
    {
        [Get("/api/v1/reservations")]
        Task<ApiResponse<List<ReservationDto>>> GetAllReservations();
    }
}
