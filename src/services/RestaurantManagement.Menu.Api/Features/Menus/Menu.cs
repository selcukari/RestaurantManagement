
using RestaurantManagement.Menu.Api.Features.Products;

namespace RestaurantManagement.Menu.Api.Features.Menus
{
    public class Menu: BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Product>? Products { get; set; }
    }
}
