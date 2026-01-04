using MassTransit;
using RestaurantManagement.Bus.Events;
using RestaurantManagement.Order.Application.Contracts.Repositories;
using RestaurantManagement.Order.Application.Contracts.UnitOfWork;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Order.Application.Consumers
{
    public class ProductNameChangedEventConsumer(ICacheService cacheService, IOrderRepository orderRepository, IUnitOfWork unitOfWork) : IConsumer<ProductNameChangedEvent>
    {
        public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
        {
            var orderItems = await orderRepository.GetOrderItemsByProductId(context.Message.ProductId);

            if (orderItems == null || orderItems.Count < 1) return;

            foreach (var orderItem in orderItems)
            {

                orderItem.UpdateProductName(context.Message.UpdatedName);

            }
            cacheService.Remove("orders");

            // 4. Tüm değişiklikleri tek seferde kaydet
            await unitOfWork.CommitAsync();
        }
    }
}
