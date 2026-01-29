namespace RestaurantManagement.Web.Dto
{
    public record ReportingDto(Guid Id, DateTime Update,
        List<ReservationReporDto>? ReservationRepors = null);
}
