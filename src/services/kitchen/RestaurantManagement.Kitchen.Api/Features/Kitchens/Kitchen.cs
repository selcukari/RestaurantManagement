namespace RestaurantManagement.Kitchen.Api.Features.Kitchens
{
    public class Kitchen: BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }

        public List<KitchenItem> Items { get; set; } = new();
    }
}
