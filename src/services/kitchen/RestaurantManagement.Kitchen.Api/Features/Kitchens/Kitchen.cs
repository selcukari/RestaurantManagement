namespace RestaurantManagement.Kitchen.Api.Features.Kitchens
{
    public class Kitchen: BaseEntity
    {
        public Kitchen()
        {
            
        }
        public Kitchen(Guid userId, List<KitchenItem> ıtems)
        {
            UserId = userId;
            Items = ıtems;
        }

        public Guid UserId { get; set; }

        public List<KitchenItem> Items { get; set; } = new();
    }
}
