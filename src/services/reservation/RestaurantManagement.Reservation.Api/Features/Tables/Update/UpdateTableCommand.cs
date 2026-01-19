namespace RestaurantManagement.Reservation.Api.Features.Tables.Update;

    public record UpdateTableCommand(Guid Id,
    int TableNumber,
    int Capacity,
    string Location,
    bool IsAvailable) : IRequestByServiceResult;
