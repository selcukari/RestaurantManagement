using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Menu.Api.Features.Products.Update
{
    public class UpdateProductCommandHandler(AppDbContext context, IMapper mapper, ICacheService cacheService)
    : IRequestHandler<UpdateProductCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var hasProduct = await context.Products.FindAsync([request.Id], cancellationToken);
            if (hasProduct == null) return ServiceResult.ErrorAsNotFound();

            hasProduct.Name = request.Name;
            hasProduct.Description = request.Description;
            hasProduct.Price = request.Price;
            hasProduct.ImageUrl = request.ImageUrl;
            hasProduct.MenumId = request.MenumId;


            context.Products.Update(hasProduct);


            await context.SaveChangesAsync(cancellationToken);

            cacheService.Remove("products");

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
