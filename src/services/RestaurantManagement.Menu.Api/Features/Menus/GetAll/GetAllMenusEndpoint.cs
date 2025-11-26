namespace RestaurantManagement.Menu.Api.Features.Menus.GetAll
{

        public class GetAllMenusQuery : IRequestByServiceResult<List<MenuDto>>;

        public class GetAllCategoryQueryHandler(AppDbContext context, IMapper mapper)
            : IRequestHandler<GetAllMenusQuery, ServiceResult<List<MenuDto>>>
        {
            public async Task<ServiceResult<List<MenuDto>>> Handle(GetAllMenusQuery request,
                CancellationToken cancellationToken)
            {
                var menus = await context.Menus.ToListAsync(cancellationToken);
                var menusAsDto = mapper.Map<List<MenuDto>>(menus);
                return ServiceResult<List<MenuDto>>.SuccessAsOk(menusAsDto);
            }
        }

        public static class GetAllMenusEndpoint
        {
            public static RouteGroupBuilder GetAllMenuGroupItemEndpoint(this RouteGroupBuilder group)
            {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllMenusQuery())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .WithName("GetAllMenu");


                return group;
            }
        }
}
