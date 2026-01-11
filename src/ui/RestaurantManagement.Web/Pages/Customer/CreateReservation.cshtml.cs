using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Customer
{
    [Authorize(Roles = "customer,instructor")]
    public class CreateReservationModel(ReservationService reservationService) : BasePageModel
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
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await reservationService.CreateReservationAsync(ViewModel);

            if (!result.IsSuccess) return ErrorPage(result, "/");

            return RedirectToPage("Index");
        }
    }
}
