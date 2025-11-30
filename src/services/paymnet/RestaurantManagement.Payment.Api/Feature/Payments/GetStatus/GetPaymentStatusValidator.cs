using FluentValidation;

namespace RestaurantManagement.Payment.Api.Feature.Payments.GetStatus
{
    public class GetPaymentStatusValidator: AbstractValidator<GetPaymentStatusRequest>
    {
        public GetPaymentStatusValidator()
        {
            RuleFor(x => x.orderCode).NotEmpty().WithMessage("OrderCode is required");
        }
    }
}
