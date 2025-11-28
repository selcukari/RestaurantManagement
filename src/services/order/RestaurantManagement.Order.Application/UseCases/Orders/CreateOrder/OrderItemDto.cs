namespace RestaurantManagement.Order.Application.UseCases.Orders.CreateOrder
{
    public record OrderItemDto(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity);
}
