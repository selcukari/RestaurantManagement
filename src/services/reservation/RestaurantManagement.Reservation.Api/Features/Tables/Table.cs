
namespace RestaurantManagement.Reservation.Api.Features.Tables
{
    public class Reservation: BaseEntity
    {
        public int TableNumber { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime Created {  get; set; }
        public bool IsAvailable { get; set; }
        public Location Location { get; set; }
        public TableStatus Status { get; set; }

    }

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
