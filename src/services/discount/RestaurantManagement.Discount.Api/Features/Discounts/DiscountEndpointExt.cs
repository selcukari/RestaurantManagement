using Asp.Versioning.Builder;
using RestaurantManagement.Discount.Api.Features.Discounts.CreateDiscount;
using RestaurantManagement.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace RestaurantManagement.Discount.Api.Features.Discounts
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("discounts").WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItemEndpoint()
                .GetDiscountByCodeGroupItemEndpoint();
        }
    }
}
