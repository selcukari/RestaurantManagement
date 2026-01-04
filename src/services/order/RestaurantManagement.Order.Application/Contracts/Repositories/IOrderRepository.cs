using RestaurantManagement.Order.Domain.Entities;

namespace RestaurantManagement.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<Guid, Domain.Entities.Order>
    {
        Task<List<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId);
        Task<List<OrderItem>> GetOrderItemsByProductId(Guid productId);

        Task SetStatus(string orderCode, Guid paymentId, OrderStatus status);
    }
}
