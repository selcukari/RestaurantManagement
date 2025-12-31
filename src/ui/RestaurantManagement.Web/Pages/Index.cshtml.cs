using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages
{
    public class IndexModel(MenuService menuService, ILogger<IndexModel> logger) : BasePageModel
    {
        public List<ProductViewModel>? Products { get; set; } = [];

        public async Task<IActionResult> OnGet()
        {
            var productsAsResult = await menuService.GetAllProductsAsync();

            if (productsAsResult.IsFail) return ErrorPage(productsAsResult);

            Products = productsAsResult.Data!;

            return Page();
        }
    }
}
