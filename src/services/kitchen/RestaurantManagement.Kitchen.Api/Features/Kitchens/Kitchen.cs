namespace RestaurantManagement.Kitchen.Api.Features.Kitchens
{
    public class Kitchen: BaseEntity
    {
        public string UserFullName { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        public List<KitchenItem> Items { get; set; } = new();
    }
}
