using Microsoft.Extensions.FileProviders;
using MassTransit;
using RestaurantManagement.Bus.Commands;
using RestaurantManagement.Bus.Events;

namespace RestaurantManagement.File.Api.Consumers
{
    public class UploadProductPictureCommandConsumer(IServiceProvider serviceProvider)
    : IConsumer<UploadProductPictureCommand>
    {
        public async Task Consume(ConsumeContext<UploadProductPictureCommand> context)
        {
            using var scope = serviceProvider.CreateScope();
            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();


            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(context.Message.FileName)}"; // .jpg

            var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);


            await System.IO.File.WriteAllBytesAsync(uploadPath, context.Message.picture);
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();


            await publishEndpoint.Publish(new ProductPictureUploadedEvent(context.Message.courseId,
                $"files/{newFileName}")); // kuyruk a atarak kuyruktan menu service dinleyecek
        }
    }
}
