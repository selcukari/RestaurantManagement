using RestaurantManagement.Menu.Api.Features.Products.Dtos;

namespace RestaurantManagement.Menu.Api.Features.Products.GetAllByUserId;

public record GetProductByUserIdQuery(Guid Id) : IRequestByServiceResult<List<ProductDto>>;

public class GetProductByIdQueryHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetProductByUserIdQuery, ServiceResult<List<ProductDto>>>
{
    public async Task<ServiceResult<List<ProductDto>>> Handle(GetProductByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var products = await context.Products.Where(x => x.UserId == request.Id)
            .ToListAsync(cancellationToken);

        var categories = await context.Menus.ToListAsync(cancellationToken);


        foreach (var product in products) product.Menum = categories.First(x => x.Id == product.MenumId);

        var coursesAsDto = mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.SuccessAsOk(coursesAsDto);
    }
}

public static class GeProductByUserIdEndpoint
    {
    public static RouteGroupBuilder GetByUserIdProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/user/{userId:guid}",
                async (IMediator mediator, Guid userId) =>
                    (await mediator.Send(new GetProductByUserIdQuery(userId))).ToGenericResult())
            .WithName("GetByUserIdProducts")
            .MapToApiVersion(1, 0);
            //.RequireAuthorization(policyNames: "InstructorPolicy");

        return group;
    }
}
