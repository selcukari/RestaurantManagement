namespace RestaurantManagement.Bus.Events
{
    public record OrderCreatedForReporingEvent(string UserFullName, DateTime Created, List<OrderCreatedForKitchenItem> items);
}
