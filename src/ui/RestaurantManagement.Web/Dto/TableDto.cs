namespace RestaurantManagement.Web.Dto
{
    public record TableDto
    (Guid Id,
    int TableNumber,
    Guid UserId,
    int Capacity,
    DateTime Created,
    bool IsAvailable,
    Location Location,
    TableStatus Status);

    public enum Location
    {
        Window,      // Pencere kenarı
        Garden,      // Bahçe
        Middle,      // Orta
        Corner,      // Köşe
        Terrace,     // Teras
        VIP          // VIP alan
    }

    public enum TableStatus
    {
        Empty,       // Boş
        Occupied,    // Dolu
        Reserved     // Rezerve
    }
}
