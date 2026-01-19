using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class UpdateTableModel(ReservationService reservationService) : BasePageModel
    {
            [BindProperty] public UpdateTableViewModel ViewModel { get; set; } = new UpdateTableViewModel();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var tableAsResult = await reservationService.GetTableAsync(id);


            if (tableAsResult.IsFail)
            {
                return RedirectToPage("/Error");
            }

            var table = tableAsResult.Data!;

            // 1. Veritabanýndan gelen veriyi ViewModel'e eþliyoruz (Mapping)
            ViewModel = new UpdateTableViewModel
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                Location = table.Location,
                IsAvailable = table.IsAvailable,
            };

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

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await reservationService.UpdateTableAsync(ViewModel);

            if (!result.IsSuccess)
            {
                return RedirectToPage("Error");
            }

            return RedirectToPage("Tables");
        }
    }
}
