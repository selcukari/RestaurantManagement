using RestaurantManagement.Menu.Api.Features.Products;

namespace RestaurantManagement.Menu.Api.Features.Menus
{
    public class Menum: BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Product>? Products { get; set; }
    }
}
