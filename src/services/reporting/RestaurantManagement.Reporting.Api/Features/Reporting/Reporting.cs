using RestaurantManagement.Reporting.Api.Repositories;

namespace RestaurantManagement.Reporting.Api.Features.Reporting
{
    public class Reporting: BaseEntity
    {
        public DateTime Update { get; set; }

        public List<ReservationRepor> ReservationRepors { get; set; } = default!;
        public List<KitchenRepor> KitchenRepors { get; set; } = default!;
        public List<PaymentRepor> PaymentRepors { get; set; } = default!;
    }
}
