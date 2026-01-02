using RestaurantManagement.Bus.Commands;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Menu.Api.Features.Products.Create
{
    public class CreateProductCommandHandler(AppDbContext context,
    IMapper mapper,
    IPublishEndpoint publishEndpoint,
    IIdentityService identityService, ICacheService cacheService) : IRequestHandler<CreateProductCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var hasMenu = await context.Menus.AnyAsync(x => x.Id == request.MenumId, cancellationToken);


            if (!hasMenu)
                return ServiceResult<Guid>.Error("Menu not found.",
                    $"The Menu with id({request.MenumId}) was not found", HttpStatusCode.NotFound);

            // daha once veri tabanda aynı isimle data var mı
            var hasProduct = await context.Products.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (hasProduct)
                return ServiceResult<Guid>.Error("Product already exists.",
                    $"The Product with name({request.Name}) already exists", HttpStatusCode.BadRequest);


            var newProduct = mapper.Map<Product>(request);
            newProduct.Created = DateTime.Now;
            newProduct.UserId = identityService.UserId;
            newProduct.Id = NewId.NextSequentialGuid(); // index performance

            newProduct.Feature = new Feature
            {
                Duration = 10, // calculate by course video
                EducatorFullName = identityService.UserName, // get by token payload
                Rating = 0
            };

            context.Products.Add(newProduct);
            await context.SaveChangesAsync(cancellationToken);

            if (request.Picture is not null)
            {
                using var memoryStream = new MemoryStream();
                await request.Picture.CopyToAsync(memoryStream, cancellationToken);

                var PictureAsByteArray = memoryStream.ToArray();


                var uploadProductPictureCommand =
                    new UploadProductPictureCommand(newProduct.Id, PictureAsByteArray, request.Picture.FileName);

                await publishEndpoint.Publish(uploadProductPictureCommand, cancellationToken);
            }

            cacheService.Remove("products");

            return ServiceResult<Guid>.SuccessAsCreated(newProduct.Id, $"/api/products/{newProduct.Id}");
        }
    }
}
