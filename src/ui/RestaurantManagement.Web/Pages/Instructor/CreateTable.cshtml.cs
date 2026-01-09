using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CreateTableModel(ReservationService reservationService) : BasePageModel
    {
        [BindProperty] public CreateTableViewModel ViewModel { get; set; } = CreateTableViewModel.Empty;

        public void OnGet()
        {
            var locations = new List<SelectListItem>
            {
                new() { Text = "Bahçe", Value = "garden" },
                new() { Text = "Pencere", Value = "window" },
                new() { Text = "Middle", Value = "middle" },
                new() { Text = "Corner", Value = "corner" },
                new() { Text = "Terrace", Value = "terrace" },
                new() { Text = "VIP", Value = "vip" }
            };

            ViewModel.SetLocationDropdownList(locations);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await reservationService.CreateTableAsync(ViewModel);

            if (!result.IsSuccess)
            {
                return RedirectToPage("Error");
            }

            return RedirectToPage("Tables");
        }
    }
}
