using RestaurantManagement.Menu.Api.Features.Products.Dtos;

namespace RestaurantManagement.Menu.Api.Features.Products.GetAll
{
    public record GetAllProductsQuery : IRequestByServiceResult<List<ProductDto>>;

    public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper)
        : IRequestHandler<GetAllProductsQuery, ServiceResult<List<ProductDto>>>
    {
        public async Task<ServiceResult<List<ProductDto>>> Handle(GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var products = await context.Products
                .ToListAsync(cancellationToken);

            var categories = await context.Menus.ToListAsync(cancellationToken);


            foreach (var product in products) product.Menum = categories.First(x => x.Id == product.MenuId);

            var coursesAsDto = mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.SuccessAsOk(coursesAsDto);
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
