using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Reservations.Create
{
    public class CreateReservationCommandHandler(AppDbContext context,
    IMapper mapper,
    IIdentityService identityService, ICacheService cacheService) : IRequestHandler<CreateReservationCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {

            // daha once veri tabanda aynı isimle data var mı
            var hasReservation = await context.Reservations.AnyAsync(x => x.TableId == request.TableId && x.ReservationDate == request.ReservationDate,
                  cancellationToken);

            if (hasReservation)
                return ServiceResult<Guid>.Error("Reservation already exists.",
                    $"The Reservation with date({request.ReservationDate.ToLongDateString()}) already exists", HttpStatusCode.BadRequest);


            var newReservation = mapper.Map<Reservation>(request);
            newReservation.Created = DateTime.Now;
            newReservation.CustomerFullName = identityService.UserName;
            newReservation.Id = NewId.NextSequentialGuid(); // index performance

        

            context.Reservations.Add(newReservation);
            await context.SaveChangesAsync(cancellationToken);


            cacheService.Remove("tables");

            return ServiceResult<Guid>.SuccessAsCreated(newReservation.Id, $"/api/reservations/{newReservation.Id}");
        }
    }
}
