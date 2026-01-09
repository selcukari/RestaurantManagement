namespace RestaurantManagement.Reservation.Api.Features.Tables.Update;
    public class UpdateTableCommandValidator: AbstractValidator<UpdateTableCommand>
    {
        public UpdateTableCommandValidator()
        {
        RuleFor(x => x.TableNumber)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Capacity).GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be at least 1.");
    }
    }
