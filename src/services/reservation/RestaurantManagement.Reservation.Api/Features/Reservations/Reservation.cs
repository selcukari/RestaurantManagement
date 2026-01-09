
namespace RestaurantManagement.Reservation.Api.Features.Reservations
{
    public class Reservation: BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Guid TableId { get; set; }
        public DateTime Created { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime {  get; set; }
        public TimeSpan EndTime { get; set; }
        public int GuestCount { get; set; }
    }
}
