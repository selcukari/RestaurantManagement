using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Pages.Instructor.Kitchen.ViewModel;
using RestaurantManagement.Web.Services;

namespace RestaurantManagement.Web.Pages.Instructor.Kitchen
{
    [Authorize]
    public class IndexModel(KitchenService kitchenService) : BasePageModel
    {
        public List<KitchenViewModel> KitchenList { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var response = await kitchenService.GetAllProductsAsync();

            if (response.IsFail) return ErrorPage(response);

            KitchenList = response.Data!;

            return Page();
        }
    }
}
