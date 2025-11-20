namespace RestaurantManagement.Menu.Api.Consumers
{
    public class ProductPictureUploadedEventConsumer(IServiceProvider serviceProvider)
    : IConsumer<Bus.Events.ProductPictureUploadedEvent>
    {
        public async Task Consume(ConsumeContext<Bus.Events.ProductPictureUploadedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var course = dbContext.Products.Find(context.Message.CourseId);
            if (course == null) throw new NotImplementedException();
            course.ImageUrl = context.Message.ImageUrl;
            await dbContext.SaveChangesAsync();
        }
    }
}
