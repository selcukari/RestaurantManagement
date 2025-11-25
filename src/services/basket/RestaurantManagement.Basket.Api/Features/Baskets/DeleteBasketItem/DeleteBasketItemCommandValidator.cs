using FluentValidation;

namespace RestaurantManagement.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandValidator: AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ProductId is required");
        }
    }
}
