namespace RestaurantManagement.Web.ViewModel
{
    public record ReportingViewModel
    (
        Guid Id,
        string Update,
        List<ReservationForCustomerViewModel>? ReservationForCustomerViewModel = null,
        List<ReservationForPreferenceViewModel>? ReservationForPreferenceViewModel = null // reservation tercih edildi

     )
    {
        public ReportingViewModel() : this(Guid.Empty, string.Empty, null, null)
        {
        }
    }
}
