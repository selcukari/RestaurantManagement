namespace RestaurantManagement.Bus.Events
{
    public record OrderCreatedItemsEvent(List<OrderCreatedForKitchenItem> items);
}
