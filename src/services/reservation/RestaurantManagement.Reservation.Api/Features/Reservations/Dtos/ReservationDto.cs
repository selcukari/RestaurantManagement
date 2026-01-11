using RestaurantManagement.Reservation.Api.Features.Tables.Dtos;

namespace RestaurantManagement.Reservation.Api.Features.Reservations.Dtos
{
    public record ReservationDto(
    Guid Id,
    string CustomerFullName,
    DateTime Created,
    DateTime ReservationDate,
    TimeSpan StartTime,
    TimeSpan EndTime,
    bool IsAvailable,
    int GuestCount,
    TableDto Table
    );
}
