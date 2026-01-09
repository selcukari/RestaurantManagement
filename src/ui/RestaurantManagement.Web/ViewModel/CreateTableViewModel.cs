using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Web.ViewModel
{
    public record CreateTableViewModel
    {
        public static CreateTableViewModel Empty => new();

        [Display(Name = "Konum Seç")] public SelectList LocationDropdownList { get; set; } = null!;

        [Display(Name = "TableNumber")] public int TableNumber { get; init; }
        [Display(Name = "Capacity")] public int Capacity { get; init; }
        [Display(Name = "Location")] public string Location { get; init; } = "window";

        public void SetLocationDropdownList(List<SelectListItem> locations)
        {
            LocationDropdownList = new SelectList(locations, "Value", "Text");
        }
    }
    public enum Location
    {
        Window,      // Pencere kenarı
        Garden,      // Bahçe
        Middle,      // Orta
        Corner,      // Köşe
        Terrace,     // Teras
        VIP          // VIP alan
    }
}
