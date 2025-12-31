using Asp.Versioning.Builder;
using RestaurantManagement.Basket.Api.Features.Baskets.AddBasketItem;
using RestaurantManagement.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using RestaurantManagement.Basket.Api.Features.Baskets.DeleteBasketItem;
using RestaurantManagement.Basket.Api.Features.Baskets.GetBasket;
using RestaurantManagement.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace RestaurantManagement.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .GetBasketGroupItemEndpoint()
                .ApplyDiscountCouponGroupItemEndpoint()
                .RemoveDiscountCouponGroupItemEndpoint().RequireAuthorization("Password"); ;
        }
    }
}
