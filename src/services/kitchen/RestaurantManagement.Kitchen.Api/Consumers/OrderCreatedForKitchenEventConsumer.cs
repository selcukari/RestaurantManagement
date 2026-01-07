using RestaurantManagement.Bus.Events;
using RestaurantManagement.Kitchen.Api.Features.Kitchens;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Kitchen.Api.Consumers
{
    public class OrderCreatedForKitchenEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedForKitchenEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedForKitchenEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var eventMessage = context.Message;

            var kitchen = new RestaurantManagement.Kitchen.Api.Features.Kitchens.Kitchen
            {
                Id = NewId.NextSequentialGuid(),
                UserId = eventMessage.UserId,
                Created = DateTime.Now,
                // Event içindeki item listesini KitchenItem listesine mapliyoruz
                Items = eventMessage.items.Select(item => new KitchenItem
                {
                    Id = item.Id, // Eğer bu ID sipariş kaleminin ID'si ise aynen aktarılabilir
                    Name = item.Name,
                    Quantity = item.Quantity
                }).ToList()
            };

            await dbContext.Kitchens.AddAsync(kitchen);

            await dbContext.SaveChangesAsync();
        }
    }
}
