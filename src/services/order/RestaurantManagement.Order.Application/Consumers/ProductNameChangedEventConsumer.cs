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
            var orders = await orderRepository.GetOrdersByProductId(context.Message.ProductId);

            if (orders.Any() != true) return;

            foreach (var order in orders)
            {
                // 2. Siparişin içindeki ilgili ürünleri bul ve ismini güncelle
                var itemsToUpdate = order.OrderItems.Where(x => x.ProductId == context.Message.ProductId);

                foreach (var item in itemsToUpdate)
                {
                    item.UpdateProductName(context.Message.UpdatedName);
                }

                // 3. Siparişi güncelle
                orderRepository.Update(order);
            }
            cacheService.Remove("orders");

            // 4. Tüm değişiklikleri tek seferde kaydet
            await unitOfWork.CommitAsync();
        }
    }
}
