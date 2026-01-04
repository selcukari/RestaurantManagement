using MassTransit;
using RestaurantManagement.Basket.Api.Features.Baskets;
using RestaurantManagement.Bus.Events;

namespace RestaurantManagement.Basket.Api.Consumers
{
    public class ProductNameChangedEventConsumer(IServiceProvider serviceProvider) : IConsumer<ProductNameChangedEvent>
    {
        public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var basketService = scope.ServiceProvider.GetRequiredService<BasketService>();
            await basketService.DeleteBasketsByProductId(context.Message.ProductId);
        }
    }
}
