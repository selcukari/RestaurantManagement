using RestaurantManagement.Menu.Api.Features.Products.Dtos;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Menu.Api.Features.Products.GetAll
{
    public record GetAllProductsQuery : IRequestByServiceResult<HashSet<ProductDto>>;

    public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper, ICacheService cacheService)
        : IRequestHandler<GetAllProductsQuery, ServiceResult<HashSet<ProductDto>>>
    {
        public async Task<ServiceResult<HashSet<ProductDto>>> Handle(GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"products";

            var productList = cacheService.Get<HashSet<ProductDto>>(cacheKey);

            if (productList?.Any() != true)
            {
                var products = await context.Products
                .ToListAsync(cancellationToken);

                var menus = await context.Menus.ToListAsync(cancellationToken);


                foreach (var product in products) product.Menum = menus.First(x => x.Id == product.MenumId);

                productList = mapper.Map<HashSet<ProductDto>>(products);

                cacheService.Set(cacheKey, productList, TimeSpan.FromDays(5));
            }

                

            return ServiceResult<HashSet<ProductDto>>.SuccessAsOk(productList);
        }
    }

    public static class GetAllProductsEndpoint
    {
        public static RouteGroupBuilder GetAllProductGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllProductsQuery())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .WithName("GetAllProducts");

            return group;
        }
    }
}
