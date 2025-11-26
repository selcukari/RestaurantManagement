using RestaurantManagement.Menu.Api.Features.Menus;

namespace RestaurantManagement.Menu.Api.Features.Products
{
    public class Product: BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime Created { get; set; }

        public Guid MenumId { get; set; }
        public Menum Menum { get; set; } = default!;

        public Feature Feature { get; set; } = default!;
    }
}
