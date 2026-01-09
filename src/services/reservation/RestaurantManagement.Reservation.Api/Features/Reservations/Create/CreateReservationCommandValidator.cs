namespace RestaurantManagement.Reservation.Api.Features.Reservations.Create
{
    public class CreateReservationCommandValidator: AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.ReservationDate)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.TableId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.GuestCount).GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be at least 1.");
        }
    }
}
