using RestaurantManagement.Shared;

namespace RestaurantManagement.Payment.Api.Feature.Payments.GetStatus
{
    public record GetPaymentStatusRequest(string orderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;
}
