using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Web.ViewModel
{
    public record UpdateProductViewModel
    {
        // Güncellenecek ürünün ID'si (Zorunludur)
        public Guid Id { get; init; }

        // Mevcut resmin yolu (Eğer yeni resim seçilmezse eskisini tutmak için)
        public string? ExistingPictureUrl { get; init; }

        [Display(Name = "Product Menu")]
        public SelectList? MenuDropdownList { get; set; }

        [Display(Name = "Product Picture")]
        public IFormFile? PictureFormFile { get; init; }

        [Display(Name = "Product Name")]
        public string Name { get; init; } = null!;

        [Display(Name = "Product Description")]
        public string Description { get; init; } = null!;

        [Display(Name = "Product Price")]
        public decimal Price { get; init; }

        [Display(Name = "Menu")]
        public Guid MenuId { get; init; }

        public void SetCategoryDropdownList(List<MenuViewModel> menus)
        {
            MenuDropdownList = new SelectList(menus, "Id", "Name", MenuId);
        }
    }
}
