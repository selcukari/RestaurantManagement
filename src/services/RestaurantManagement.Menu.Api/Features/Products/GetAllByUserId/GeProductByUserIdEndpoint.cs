using RestaurantManagement.Menu.Api.Features.Products.Dtos;

namespace RestaurantManagement.Menu.Api.Features.Products.GetAllByUserId;

public record GetProductByUserIdQuery(Guid Id) : IRequestByServiceResult<HashSet<ProductDto>>;

public class GetProductByIdQueryHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetProductByUserIdQuery, ServiceResult<HashSet<ProductDto>>>
{
    public async Task<ServiceResult<HashSet<ProductDto>>> Handle(GetProductByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var products = await context.Products.Where(x => x.UserId == request.Id)
            .ToListAsync(cancellationToken);

        var categories = await context.Menus.ToListAsync(cancellationToken);


        foreach (var product in products) product.Menum = categories.First(x => x.Id == product.MenuId);

        var coursesAsDto = mapper.Map<HashSet<ProductDto>>(products);
        return ServiceResult<HashSet<ProductDto>>.SuccessAsOk(coursesAsDto);
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
