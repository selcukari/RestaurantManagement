using RestaurantManagement.Payment.Api.Repositories;

namespace RestaurantManagement.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdResponse(
    Guid Id,
    string OrderCode,
    string Amount,
    DateTime Created,
    PaymentStatus Status);
}
