namespace RestaurantManagement.Reservation.Api.Features.Tables.Dtos
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
