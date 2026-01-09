namespace RestaurantManagement.Web.Dto
{
    public record ReservationDto(
        Guid Id,
    Guid CustomerId,
    DateTime Created,
    DateTime ReservationDate,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int GuestCount,
    TableDto Table
        );
}
