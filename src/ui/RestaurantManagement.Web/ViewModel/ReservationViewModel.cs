
namespace RestaurantManagement.Web.ViewModel
{
    public record ReservationViewModel
    (
        Guid Id,
        string CustomerFullName,
        string Created,
        string ReservationDate,
        string StartTime,
        string EndTime,
        int GuestCount,

        Guid? TableId,
        int TableNumber // table dan geliyor
     );
}
