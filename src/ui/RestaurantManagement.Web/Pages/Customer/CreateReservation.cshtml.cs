using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Customer
{
    [Authorize(Roles = "customer")]
    public class CreateReservationModel(ReservationService reservationService) : PageModel
    {
        [BindProperty] public CreateReservationViewModel ViewModel { get; set; } = CreateReservationViewModel.Empty;

        public async Task OnGetAsync()
        {
            var tablesResult = await reservationService.GetTablesAsync();


            if (tablesResult.IsFail)
            {
                //TODO : redirect error page
            }

            ViewModel.SetTableDropdownList(tablesResult.Data!);
        }
    }
}
