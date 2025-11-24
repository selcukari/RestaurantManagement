namespace RestaurantManagement.Menu.Api.Features.Menus.GetById;

public record GetMenuByIdQuery(Guid Id) : IRequestByServiceResult<MenuDto>;

public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetMenuByIdQuery, ServiceResult<MenuDto>>
{
    public async Task<ServiceResult<MenuDto>> Handle(GetMenuByIdQuery request,
        CancellationToken cancellationToken)
    {
        var hasMenu = await context.Menus.FindAsync(request.Id, cancellationToken);

        if (hasMenu == null)
            return ServiceResult<MenuDto>.Error("Menu not found",
                $"The Menu with id({request.Id}) was not found", HttpStatusCode.NotFound);

        var menuAsDto = mapper.Map<MenuDto>(hasMenu);
        return ServiceResult<MenuDto>.SuccessAsOk(menuAsDto);
    }
}
  public static class GetMenuByIdEndpoint
  {
    public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                    (await mediator.Send(new GetMenuByIdQuery(id))).ToGenericResult())
            .MapToApiVersion(1, 0)
            .WithName("GetByIdMenu");


        return group;
    }
  }
