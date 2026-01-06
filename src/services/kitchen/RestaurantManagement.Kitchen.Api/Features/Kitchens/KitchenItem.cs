namespace RestaurantManagement.Kitchen.Api.Features.Kitchens
{
    public class KitchenItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
