using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor,customer")]
    public class UpdateReservationModel(ReservationService reservationService) : BasePageModel
    {
        [BindProperty] public UpdateReservationViewModel ViewModel { get; set; } = new UpdateReservationViewModel();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var tablesResult = await reservationService.GetTablesAsync();
            var reservationAsResult = await reservationService.GetReservationAsync(id);


            if (tablesResult.IsFail || reservationAsResult.IsFail)
            {
                return RedirectToPage("/Error");
            }

            var product = reservationAsResult.Data!;

            // 1. Veritabanýndan gelen veriyi ViewModel'e eþliyoruz (Mapping)
            ViewModel = new UpdateReservationViewModel
            {
                Id = product.Id,
                ReservationDate = DateTime.Parse(product.ReservationDate),
                StartTime = TimeSpan.Parse(product.StartTime),
                EndTime = TimeSpan.Parse(product.EndTime),
                GuestCount = product.GuestCount,
                TableId = (Guid)product?.TableId
            };

            // 2. Dropdown listesini dolduruyoruz
            ViewModel.SetTableDropdownList(tablesResult.Data!);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //var result = await reservationService.UpdateProductAsync(ViewModel);

            //if (!result.IsSuccess)
            //{
            //    return RedirectToPage("Error");
            //}

            return RedirectToPage("Reservations");
        }
    }
}
