namespace RestaurantManagement.Bus.Events
{
    public record OrderCreatedForKitchenEvent(string UserFullName, List<OrderCreatedForKitchenItem> items);
    public record OrderCreatedForKitchenItem(Guid Id, string Name, int Quantity);
}
