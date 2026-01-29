using Refit;
using RestaurantManagement.Web.Dto;

namespace RestaurantManagement.Web.Services.Refit
{
    public interface IReportingRefitService
    {
        [Get("/api/v1/reportings")]
        Task<ApiResponse<ReportingDto>> GetAllReportings();
    }
}
