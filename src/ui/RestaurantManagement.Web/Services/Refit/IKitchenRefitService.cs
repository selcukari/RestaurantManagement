using Refit;
using RestaurantManagement.Web.Pages.Kitchen.Dto;

namespace RestaurantManagement.Web.Services.Refit
{
    public interface IKitchenRefitService
    {
        [Get("/api/v1/kitchens")]
        Task<ApiResponse<List<KitchenDto>>> GetAllKitchens();
    }
}
