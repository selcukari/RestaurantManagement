namespace RestaurantManagement.Reservation.Api.Features.Tables.Create
{
    public class CreateReservationCommandValidator: AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Capacity).GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be at least 1.");
        }
    }
}
