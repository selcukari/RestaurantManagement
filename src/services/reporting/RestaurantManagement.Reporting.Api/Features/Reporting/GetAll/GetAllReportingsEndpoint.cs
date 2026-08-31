using RestaurantManagement.Reporting.Api.Features.Reporting.Dtos;
using RestaurantManagement.Reporting.Api.Repositories;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reporting.Api.Features.Reporting.GetAll
{
    public record GetAllReportingsQuery : IRequestByServiceResult<ReportingDto>;

    public class GetAllReportingsQueryHandler(AppDbContext context, IMapper mapper, ICacheService cacheService)
        : IRequestHandler<GetAllReportingsQuery, ServiceResult<ReportingDto>>
    {
        public async Task<ServiceResult<ReportingDto>> Handle(GetAllReportingsQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"reportings";

            var reporting = cacheService.Get<ReportingDto>(cacheKey);

            if (reporting == null)
            {
                var getReservation = await context.Reportings
                .Include(x => x.ReservationRepors).AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

                reporting = mapper.Map<ReportingDto>(getReservation);

                cacheService.Set(cacheKey, reporting, TimeSpan.FromDays(5));
            }

            return ServiceResult<ReportingDto>.SuccessAsOk(reporting);
        }
    }

    public static class GetAllReportingsEndpoint
    {
        public static RouteGroupBuilder GetAllReportingGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllReportingsQuery())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .WithName("GetAllReporing");/*.RequireAuthorization(policyNames: "Password");*/

            return group;
        }
    }
}
