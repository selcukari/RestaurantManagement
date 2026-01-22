
using RestaurantManagement.Reservation.Api.Features.Reservations.Dtos;

namespace RestaurantManagement.Reservation.Api.Features.Reservations.GetById
{
    public record GetReservationByIdQuery(Guid Id) : IRequestByServiceResult<ReservationDto>;

    public class GetProductByIdQueryHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetReservationByIdQuery, ServiceResult<ReservationDto>>
    {
        public async Task<ServiceResult<ReservationDto>> Handle(GetReservationByIdQuery request,
            CancellationToken cancellationToken)
        {
            var hasReservation = await context.Reservations.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


            if (hasReservation is null)
                return ServiceResult<ReservationDto>.Error("Reservation not found",
                    $"The Product with id({request.Id}) was not found", HttpStatusCode.NotFound);

            var table = await context.Tables.FindAsync(hasReservation.TableId, cancellationToken);

            hasReservation.Table = table!;


            var reservationAsDto = mapper.Map<ReservationDto>(hasReservation);
            return ServiceResult<ReservationDto>.SuccessAsOk(reservationAsDto);
        }
    }

    public static class GetReservationByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdReservationGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                    async (IMediator mediator, Guid id) =>
                        (await mediator.Send(new GetReservationByIdQuery(id))).ToGenericResult())
                .WithName("GetByIdReservation")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
