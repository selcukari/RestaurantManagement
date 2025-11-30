using Asp.Versioning.Builder;
using RestaurantManagement.Payment.Api.Feature.Payments.Create;
using RestaurantManagement.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;
using RestaurantManagement.Payment.Api.Feature.Payments.GetStatus;

namespace RestaurantManagement.Payment.Api.Feature.Payments
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("payments").WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItemEndpoint().GetAllPaymentsByUserIdGroupItemEndpoint()
                .GetPaymentStatusGroupItemEndpoint();
        }
    }
}
