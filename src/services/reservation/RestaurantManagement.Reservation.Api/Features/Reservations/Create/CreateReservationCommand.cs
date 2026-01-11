namespace RestaurantManagement.Reservation.Api.Features.Reservations.Create
{
    public record CreateReservationCommand : IRequestByServiceResult
    {
        public Guid TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int GuestCount { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
