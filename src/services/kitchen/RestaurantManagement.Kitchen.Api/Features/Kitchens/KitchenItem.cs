namespace RestaurantManagement.Kitchen.Api.Features.Kitchens
{
    public class KitchenItem
    {
        public KitchenItem(Guid id, string name, int quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
