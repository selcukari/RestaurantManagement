namespace RestaurantManagement.Web.Dto
{
    public record ReservationDto(
        Guid Id,
    string CustomerFullName,
    DateTime Created,
    DateTime ReservationDate,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int GuestCount,
    TableDto Table
        );
}
