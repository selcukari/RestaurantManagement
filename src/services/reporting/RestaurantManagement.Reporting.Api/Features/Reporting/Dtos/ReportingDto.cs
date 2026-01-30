using RestaurantManagement.Reporting.Api.Repositories;

namespace RestaurantManagement.Reporting.Api.Features.Reporting.Dtos
{
    public record ReportingDto(Guid Id, DateTime Update,
        List<ReservationReporDto>? ReservationRepors = null,
        List<KitchenReporDto>? KitchenRepors = null,
        List<PaymentReporDto>? PaymentRepors = null
     );
}
