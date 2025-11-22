namespace RestaurantManagement.Menu.Api.Features.Menus.Create
{
    public class CreateMenuCommandValidator: AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Length(4, 25).WithMessage("{PropertyName} must be between 4 and 25 characters");
        }
    }
}
