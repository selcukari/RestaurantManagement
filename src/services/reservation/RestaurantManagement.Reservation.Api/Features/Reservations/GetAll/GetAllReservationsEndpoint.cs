using RestaurantManagement.Reservation.Api.Features.Reservations.Dtos;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Reservations.GetAll
{
    public record GetAllReservationsQuery : IRequestByServiceResult<HashSet<ReservationDto>>;

    public class GetAllReservationsQueryHandler(AppDbContext context, IMapper mapper, ICacheService cacheService)
        : IRequestHandler<GetAllReservationsQuery, ServiceResult<HashSet<ReservationDto>>>
    {
        public async Task<ServiceResult<HashSet<ReservationDto>>> Handle(GetAllReservationsQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"reservations";

            var reservationList = cacheService.Get<HashSet<ReservationDto>>(cacheKey);

            if (reservationList?.Any() != true)
            {
                var reservations = await context.Reservations.OrderByDescending(x => x.Created)
                .ToListAsync(cancellationToken);

                var tables = await context.Tables.Where(x => x.IsAvailable).ToListAsync(cancellationToken);

                foreach (var reservation in reservations) reservation.Table = tables.First(x => x.Id == reservation.TableId);

                reservationList = mapper.Map<HashSet<ReservationDto>>(tables);

                cacheService.Set(cacheKey, reservationList, TimeSpan.FromDays(10));
            }


            return ServiceResult<HashSet<ReservationDto>>.SuccessAsOk(reservationList);
        }
    }

    public static class GetAllReservationsEndpoint
    {
        public static RouteGroupBuilder GetAllReservationGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllReservationsQuery())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .WithName("GetAllReservations");

            return group;
        }
    }
}
