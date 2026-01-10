using Refit;
using RestaurantManagement.Web.Dto;

namespace RestaurantManagement.Web.Services.Refit
{
    public interface IReservationRefitService
    {
        [Get("/api/v1/reservations")]
        Task<ApiResponse<List<ReservationDto>>> GetAllReservations();

        [Get("/api/v1/tables")]
        Task<ApiResponse<List<TableDto>>> GetTablesAsync();

        [Post("/api/v1/tables")]
        Task<ApiResponse<object>> AddTableItemAsync(AddTableRequest model);

        [Post("/api/v1/reservations")]
        Task<ApiResponse<object>> AddReservationItemAsync(AddReservationRequest model);
    }
}
