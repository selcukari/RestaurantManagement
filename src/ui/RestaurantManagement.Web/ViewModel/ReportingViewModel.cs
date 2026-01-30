using RestaurantManagement.Web.Pages.Order.ViewModel;

namespace RestaurantManagement.Web.ViewModel
{
    public record ReportingViewModel
    (
        Guid Id,
        string Update,
        List<ReservationForCustomerViewModel>? ReservationForCustomerViewModel = null,
        List<ReservationForPreferenceViewModel>? ReservationForPreferenceViewModel = null, // reservation tercih edildi
        List<KitchenForProductViewModel>? KitchenForProductViewModel = null,
        List<PaymentForReportViewModel>? PaymentForReportViewModel = null

     )
    {
        public ReportingViewModel() : this(Guid.Empty, string.Empty, null, null, null, null)
        {
        }
    }
}
