using RestaurantManagement.Reservation.Api.Features.Tables.Dtos;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Tables.GetAll
{
    public record GetAllTablesQuery : IRequestByServiceResult<HashSet<ReservationDto>>;

    public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper, ICacheService cacheService)
        : IRequestHandler<GetAllTablesQuery, ServiceResult<HashSet<ReservationDto>>>
    {
        public async Task<ServiceResult<HashSet<ReservationDto>>> Handle(GetAllTablesQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"tables";

            var tableList = cacheService.Get<HashSet<ReservationDto>>(cacheKey);

            if (tableList?.Any() != true)
            {
                var tables = await context.Tables.Where(x => x.IsAvailable)
                .ToListAsync(cancellationToken);

                tableList = mapper.Map<HashSet<ReservationDto>>(tables);

                cacheService.Set(cacheKey, tableList, TimeSpan.FromDays(10));
            }

                

            return ServiceResult<HashSet<ReservationDto>>.SuccessAsOk(tableList);
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
