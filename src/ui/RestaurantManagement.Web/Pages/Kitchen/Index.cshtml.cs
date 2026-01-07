using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Pages.Kitchen.ViewModel;

namespace RestaurantManagement.Web.Pages.Kitchen
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        public KitchenPageViewModel Kitchen { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {

            return Page();
        }
    }
}
