namespace RestaurantManagement.Reservation.Api.Features.Tables.Dtos
{
    public record TableDto(
    Guid Id,
    int TableNumber,
    int Capacity,
    string UserFullName,
    DateTime Created,
    bool IsAvailable,
    Location Location,
    TableStatus Status
    );
}
