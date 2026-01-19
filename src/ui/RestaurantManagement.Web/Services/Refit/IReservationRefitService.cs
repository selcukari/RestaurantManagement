using Refit;
using RestaurantManagement.Web.Dto;

namespace RestaurantManagement.Web.Services.Refit
{
    public interface IReservationRefitService
    {
        [Get("/api/v1/reservations")]
        Task<ApiResponse<List<ReservationDto>>> GetAllReservations();

        [Get("/api/v1/reservations/{id}")]
        Task<ApiResponse<ReservationDto>> GetReservation(Guid id);

        [Get("/api/v1/tables")]
        Task<ApiResponse<List<TableDto>>> GetTablesAsync();

        [Get("/api/v1/tables/{id}")]
        Task<ApiResponse<TableDto>> GetTableAsync(Guid id);

        [Put("/api/v1/tables")]
        Task<ApiResponse<object>> UpdateTableAsync(UpdateTableRequest request);

        [Post("/api/v1/tables")]
        Task<ApiResponse<object>> AddTableItemAsync(AddTableRequest model);

        [Post("/api/v1/reservations")]
        Task<ApiResponse<object>> AddReservationItemAsync(AddReservationRequest model);

        [Delete("/api/v1/tables/{Id}")]
        Task<ApiResponse<object>> DeleteTableAsync(Guid Id);

        [Delete("/api/v1/reservations/{Id}")]
        Task<ApiResponse<object>> DeleteReservationAsync(Guid Id);
    }
}
