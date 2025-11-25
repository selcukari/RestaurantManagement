using FluentValidation;

namespace RestaurantManagement.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandValidator: AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("ProductName is required");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
            RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
        }
    }
}
