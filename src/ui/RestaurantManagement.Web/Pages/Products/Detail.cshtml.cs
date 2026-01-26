using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Products
{
    [AllowAnonymous]
    public class DetailModel(MenuService menuService) : BasePageModel
    {
        public ProductViewModel? Product { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var productAsResult = await menuService.GetProduct(id);

            if (productAsResult.IsFail) return ErrorPage(productAsResult);

            Product = productAsResult.Data!;
            return Page();
        }
    }
}
