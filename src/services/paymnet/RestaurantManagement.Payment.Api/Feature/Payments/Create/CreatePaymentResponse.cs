namespace RestaurantManagement.Payment.Api.Feature.Payments.Create
{
    public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);
}
