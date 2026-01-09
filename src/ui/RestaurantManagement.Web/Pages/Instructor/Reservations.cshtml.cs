using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Instructor
{
    [Authorize]
    public class ReservationsModel(ReservationService reservationService) : BasePageModel
    {
        public List<ReservationViewModel> ReservationViewModels { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await reservationService.GetAllReservationsAsync();

            if (result.IsFail)
            {
                return ErrorPage(result);
            }

            ReservationViewModels = result.Data!;

            return Page();
        }
    }
}
