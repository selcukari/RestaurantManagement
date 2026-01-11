
namespace RestaurantManagement.Reservation.Api.Features.Reservations
{
    public class Reservation: BaseEntity
    {
        public string CustomerFullName { get; set; } = string.Empty;

        public RestaurantManagement.Reservation.Api.Features.Tables.Table Table { get; set; } = default!;
        public Guid TableId { get; set; }

        public DateTime Created { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime {  get; set; }
        public TimeSpan EndTime { get; set; }
        public int GuestCount { get; set; }
        public bool IsAvailable { get; set; }
    }
}
