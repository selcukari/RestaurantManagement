using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Instructor
{
    public class ProductModel(MenuService menuService) : PageModel
    {
        public List<ProductViewModel> ProductViewModels { get; set; } = null!;

        public async Task OnGetAsync()
        {
            var result = await menuService.GetProductByUserId();

            if (result.IsFail)
            {
                //TODO : redirect error page
            }

            ProductViewModels = result.Data!;
        }
        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            var result = await menuService.DeleteAsync(id);
            if (result.IsFail)
            {
                //TODO : redirect error page
            }

            return RedirectToPage();
        }
    }
}
