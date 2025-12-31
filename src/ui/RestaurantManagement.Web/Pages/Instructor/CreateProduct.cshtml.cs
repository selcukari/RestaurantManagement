using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CreateProductModel(MenuService menuservice) : PageModel
    {
        [BindProperty] public CreateProductViewModel ViewModel { get; set; } = CreateProductViewModel.Empty;

        public async Task OnGetAsync()
        {
            var categoriesResult = await menuservice.GetMenusAsync();


            if (categoriesResult.IsFail)
            {
                //TODO : redirect error page
            }

            ViewModel.SetCategoryDropdownList(categoriesResult.Data!);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await menuservice.CreateProductAsync(ViewModel);

            if (!result.IsSuccess)
            {
                //TODO : Show error
            }

            return RedirectToPage("Products");
        }
    }
}
