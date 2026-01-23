using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Customer
{
    [Authorize(Roles = "customer")]
    public class IndexModel(ReservationService reservationService) : BasePageModel
    {
        public List<ReservationViewModel> ReservationViewModels { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await reservationService.GetAllByIdReservationsAsync();

            if (result.IsFail)
            {
                return ErrorPage(result);
            }

            ReservationViewModels = result.Data!;

            return Page();
        }
    }
}
