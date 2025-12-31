using Refit;
using RestaurantManagement.Web.Pages.Basket.Dto;

namespace RestaurantManagement.Web.Services.Refit
{
    public interface IDiscountRefitService
    {
        [Get("/api/v1/discounts/{coupon}")]
        Task<ApiResponse<GetDiscountByCouponResponse>> GetDiscountByCoupon(string coupon);
    }
}
