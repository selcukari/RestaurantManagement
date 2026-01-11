using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class TablesModel(ReservationService reservationService) : BasePageModel
    {
        public List<TableViewModel> TableViewModels { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await reservationService.GetTablesAsync();

            if (result.IsFail)
            {
                return RedirectToPage("Error");
            }

            TableViewModels = result.Data!;

            return Page();
        }
        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            var result = await reservationService.DeleteTableAsync(id);
            if (result.IsFail)
            {
                return RedirectToPage("Error");
            }

            return RedirectToPage();
        }
    }
}
