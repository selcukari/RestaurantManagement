namespace RestaurantManagement.Reservation.Api.Features.Tables.Dtos
{
    public record ReservationDto(
    Guid Id,
    int TableNumber,
    Guid UserId,
    int Capacity,
    DateTime Created,
    bool IsAvailable,
    Location Location,
    TableStatus Status
    );
}
