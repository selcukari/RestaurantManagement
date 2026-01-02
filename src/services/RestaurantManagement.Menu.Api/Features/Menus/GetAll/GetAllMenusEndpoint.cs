using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Menu.Api.Features.Menus.GetAll
{

        public class GetAllMenusQuery : IRequestByServiceResult<List<MenuDto>>;

        public class GetAllCategoryQueryHandler(AppDbContext context, IMapper mapper, ICacheService cacheService)
            : IRequestHandler<GetAllMenusQuery, ServiceResult<List<MenuDto>>>
        {
            public async Task<ServiceResult<List<MenuDto>>> Handle(GetAllMenusQuery request,
                CancellationToken cancellationToken)
            {
                var cacheKey = $"menus";

                var menuList = cacheService.Get<List<MenuDto>>(cacheKey);

                if (menuList == null || menuList.Count == 0)
                {
                    var menus = await context.Menus.ToListAsync(cancellationToken);
                    menuList = mapper.Map<List<MenuDto>>(menus);
                    
                    cacheService.Set(cacheKey, menuList, TimeSpan.FromDays(10));
                }


             return ServiceResult<List<MenuDto>>.SuccessAsOk(menuList);
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
                .WithName("GetAllMenu").RequireAuthorization(policyNames: "ClientCredential");

                return group;
            }
        }
}
