using RestaurantManagement.Bus.Events;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Menu.Api.Consumers
{
    public class OrderCreatedItemsEventConsumer(IServiceProvider serviceProvider, ICacheService cacheService)
    : IConsumer<OrderCreatedItemsEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedItemsEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Configure MongoDB to not use transactions
            dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

            List<OrderCreatedForKitchenItem> items = context.Message.items;

            foreach (var item in items)
            {
                var product = dbContext.Products.Find(item.Id);
                if (product == null) throw new NotImplementedException();
                product.Quantity -= item.Quantity;
            }

            await dbContext.SaveChangesAsync();

            cacheService.Remove("products");
        }
    }
}
