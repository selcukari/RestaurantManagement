namespace RestaurantManagement.Reservation.Api.Features.Tables.Update;

    public record UpdateTableCommand(Guid Id,
    int TableNumber,
    int Capacity,
    Location Location,
    bool IsAvailable,
    TableStatus Status) : IRequestByServiceResult;
