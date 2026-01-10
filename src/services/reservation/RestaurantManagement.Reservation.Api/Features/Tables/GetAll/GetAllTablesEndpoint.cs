using RestaurantManagement.Reservation.Api.Features.Tables.Dtos;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Tables.GetAll
{
    public record GetAllTablesQuery : IRequestByServiceResult<HashSet<TableDto>>;

    public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper, ICacheService cacheService)
        : IRequestHandler<GetAllTablesQuery, ServiceResult<HashSet<TableDto>>>
    {
        public async Task<ServiceResult<HashSet<TableDto>>> Handle(GetAllTablesQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"tables";

            var tableList = cacheService.Get<HashSet<TableDto>>(cacheKey);

            if (tableList?.Any() != true)
            {
                var tables = await context.Tables.Where(x => x.IsAvailable)
                .ToListAsync(cancellationToken);

                tableList = mapper.Map<HashSet<TableDto>>(tables);

                cacheService.Set(cacheKey, tableList, TimeSpan.FromDays(10));
            }

            return ServiceResult<HashSet<TableDto>>.SuccessAsOk(tableList);
        }
    }

    public static class GetAllTablesEndpoint
    {
        public static RouteGroupBuilder GetAllTableGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllTablesQuery())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .WithName("GetAllTables");

            return group;
        }
    }
}
