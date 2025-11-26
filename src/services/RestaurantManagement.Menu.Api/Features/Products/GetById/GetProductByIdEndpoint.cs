using RestaurantManagement.Menu.Api.Features.Products.Dtos;

namespace RestaurantManagement.Menu.Api.Features.Products.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequestByServiceResult<ProductDto>;

    public class GetProductByIdQueryHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, ServiceResult<ProductDto>>
    {
        public async Task<ServiceResult<ProductDto>> Handle(GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var hasProduct = await context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


            if (hasProduct is null)
                return ServiceResult<ProductDto>.Error("Product not found",
                    $"The Product with id({request.Id}) was not found", HttpStatusCode.NotFound);

            var menu = await context.Menus.FindAsync(hasProduct.MenumId, cancellationToken);

            hasProduct.Menum = menu!;


            var courseAsDto = mapper.Map<ProductDto>(hasProduct);
            return ServiceResult<ProductDto>.SuccessAsOk(courseAsDto);
        }
    }

    public static class GetProductByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdProductGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                    async (IMediator mediator, Guid id) =>
                        (await mediator.Send(new GetProductByIdQuery(id))).ToGenericResult())
                .WithName("GetByIdProduct")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
