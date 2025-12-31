using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Web.ViewModel
{
    public record CreateProductViewModel
    {
        public static CreateProductViewModel Empty => new();


        [Display(Name = "Product Menu")] public SelectList MenuDropdownList { get; set; } = null!;


        [Display(Name = "Product Picture")] public IFormFile? PictureFormFile { get; init; }


        [Display(Name = "Product Name")] public string Name { get; init; } = null!;


        [Display(Name = "Product Description")] public string Description { get; init; } = null!;


        [Display(Name = "Product Price")] public decimal Price { get; init; }

        public Guid? MenuId { get; init; }


        public void SetCategoryDropdownList(List<MenuViewModel> menus)
        {
            MenuDropdownList = new SelectList(menus, "Id", "Name");
        }
    }
}
