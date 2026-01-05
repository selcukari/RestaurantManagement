namespace RestaurantManagement.Menu.Api.Features.Products.Update
{
    public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be at least 1.");

            RuleFor(x => x.MenumId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
