namespace RestaurantManagement.Web.Dto
{
    public record AddReservationRequest(
        Guid TableId, DateTime ReservationDate, TimeSpan StartTime, TimeSpan EndTime, int GuestCount);
}
