using RestaurantManagement.Bus.Events;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Menu.Api.Features.Products.Update
{
    public class UpdateProductCommandHandler(AppDbContext context, IMapper mapper, ICacheService cacheService, IPublishEndpoint publishEndpoint)
    : IRequestHandler<UpdateProductCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var hasProduct = await context.Products.FindAsync([request.Id], cancellationToken);
            if (hasProduct == null) return ServiceResult.ErrorAsNotFound();

            hasProduct.Name = request.Name;
            hasProduct.Description = request.Description;
            hasProduct.Price = request.Price;
            hasProduct.Quantity = request.Quantity;
            hasProduct.ImageUrl = request.ImageUrl;
            hasProduct.MenumId = request.MenumId;

            hasProduct.Feature.Calorie = request.Calorie;

            context.Products.Update(hasProduct);


            await context.SaveChangesAsync(cancellationToken);

            cacheService.Remove("products");

            await publishEndpoint.Publish(new ProductNameChangedEvent(request.Id, request.Name), cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
