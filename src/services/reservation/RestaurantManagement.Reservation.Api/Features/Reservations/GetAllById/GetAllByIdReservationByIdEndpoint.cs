
using RestaurantManagement.Reservation.Api.Features.Reservations.Dtos;

namespace RestaurantManagement.Reservation.Api.Features.Reservations.GetAllById
{
    public record GetAllReservationByIdQuery(Guid Id) : IRequestByServiceResult<List<ReservationDto>>;

    public class GetAllReservationByIdQueryHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetAllReservationByIdQuery, ServiceResult<List<ReservationDto>>>
    {
        public async Task<ServiceResult<List<ReservationDto>>> Handle(GetAllReservationByIdQuery request,
            CancellationToken cancellationToken)
        {
            var reservations = await context.Reservations.AsNoTracking().Where(x => x.CustomerId == request.Id).ToListAsync(cancellationToken);


            if (reservations is null)
                return ServiceResult<List<ReservationDto>>.Error("reservations not found",
                    $"The reservation with id({request.Id}) was not found", HttpStatusCode.NotFound);

            var tables = await context.Tables.AsNoTracking().Where(x => x.IsAvailable).ToListAsync(cancellationToken);

            foreach (var reservation in reservations) reservation.Table = tables.First(x => x.Id == reservation.TableId);

            var reservationsAsDto = mapper.Map<List<ReservationDto>>(reservations);

            return ServiceResult<List<ReservationDto>>.SuccessAsOk(reservationsAsDto);
        }
    }

    public static class GetAllByIdReservationByIdEndpoint
    {
        public static RouteGroupBuilder GetAllByIdReservationGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}",
                    async (IMediator mediator, Guid userId) =>
                        (await mediator.Send(new GetAllReservationByIdQuery(userId))).ToGenericResult())
                .WithName("GetAllByIdReservation")
                .MapToApiVersion(1, 0)
                .RequireAuthorization(policyNames: "CustomerPolicy"); // sadece muşteri

            return group;
        }
    }
}
