using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Menu.Api.Consumers
{
    public class ProductPictureUploadedEventConsumer(IServiceProvider serviceProvider, ICacheService cacheService)
    : IConsumer<Bus.Events.ProductPictureUploadedEvent>
    {
        public async Task Consume(ConsumeContext<Bus.Events.ProductPictureUploadedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var product = dbContext.Products.Find(context.Message.ProductId);
            if (product == null) throw new NotImplementedException();
            product.ImageUrl = context.Message.ImageUrl;

            cacheService.Remove("products");

            await dbContext.SaveChangesAsync();
        }
    }
}
