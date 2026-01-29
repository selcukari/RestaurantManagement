namespace RestaurantManagement.Web.Dto
{
    public class ReservationReporDto
    {
        public Guid Id { get; set; }

        public string CustomerFullName { get; set; } = string.Empty;

        public Guid TableId { get; set; }
        public Guid CustomerId { get; set; }

        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int GuestCount { get; set; }
    }
}
