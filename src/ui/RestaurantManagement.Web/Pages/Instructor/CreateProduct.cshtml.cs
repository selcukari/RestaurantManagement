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
            var menusResult = await menuservice.GetMenusAsync();


            if (menusResult.IsFail)
            {
                //TODO : redirect error page
            }

            ViewModel.SetCategoryDropdownList(menusResult.Data!);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await menuservice.CreateProductAsync(ViewModel);

            if (!result.IsSuccess)
            {
                return RedirectToPage("Error");
            }

            return RedirectToPage("Products");
        }
    }
}
