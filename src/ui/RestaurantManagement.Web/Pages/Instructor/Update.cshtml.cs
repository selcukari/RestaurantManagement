using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class UpdateModel(MenuService menuService) : BasePageModel
    {
        [BindProperty] public UpdateProductViewModel ViewModel { get; set; } = new UpdateProductViewModel();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var categoriesResult = await menuService.GetMenusAsync();
            var productAsResult = await menuService.GetProduct(id);


            if (categoriesResult.IsFail || productAsResult.IsFail)
            {
                return RedirectToPage("/Error");
            }

            var product = productAsResult.Data!;

            // 1. Veritabanýndan gelen veriyi ViewModel'e eþliyoruz (Mapping)
            ViewModel = new UpdateProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                MenuId = (Guid)product?.MenuId!,
                ExistingPictureUrl = product.ImageUrl // Mevcut resim yolu
            };

            // 2. Dropdown listesini dolduruyoruz
            ViewModel.SetCategoryDropdownList(categoriesResult.Data!);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await menuService.UpdateProductAsync(ViewModel);

            if (!result.IsSuccess)
            {
                return RedirectToPage("Error");
            }

            return RedirectToPage("Products");
        }
    }
}
