namespace RestaurantManagement.Bus.Events
{
    public record OrderCreatedForKitchenEvent(Guid UserId, List<OrderCreatedForKitchenItem> items);
    public record OrderCreatedForKitchenItem(Guid Id, string Name, int Quantity);
}
