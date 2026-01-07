using RestaurantManagement.Kitchen.Api.Features.Kitchens.Dto;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Kitchen.Api.Features.Kitchens.GetAll
{
    public class GetAllKitchensQuery : IRequestByServiceResult<List<KitchenDto>>;

    public class GetAllCategoryQueryHandler(AppDbContext context, IMapper mapper, ICacheService cacheService)
            : IRequestHandler<GetAllKitchensQuery, ServiceResult<List<KitchenDto>>>
    {
        public async Task<ServiceResult<List<KitchenDto>>> Handle(GetAllKitchensQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"menus";

            var kitchenList = cacheService.Get<List<KitchenDto>>(cacheKey);

            if (kitchenList == null || kitchenList.Count == 0)
            {
                var kitchens = await context.Kitchens.ToListAsync(cancellationToken);
                kitchenList = mapper.Map<List<KitchenDto>>(kitchens);

                cacheService.Set(cacheKey, kitchenList, TimeSpan.FromDays(5));
            }


            return ServiceResult<List<KitchenDto>>.SuccessAsOk(kitchenList);
        }
    }

    public static class GetAllKitchensEndpoint
    {
        public static RouteGroupBuilder GetAllKitchenGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllKitchensQuery())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .WithName("GetAllKitchen");

            return group;
        }
    }
}
