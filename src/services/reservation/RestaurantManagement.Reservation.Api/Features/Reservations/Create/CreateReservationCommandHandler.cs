using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Reservations.Create
{
    public class CreateReservationCommandHandler(AppDbContext context,
    IMapper mapper,
    IIdentityService identityService, ICacheService cacheService) : IRequestHandler<CreateReservationCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

            // daha once veri tabanda aynı isimle data var mı
            var hasReservation = await context.Reservations.AnyAsync(x => x.TableId == request.TableId && x.ReservationDate == request.ReservationDate,
                  cancellationToken);

            if (hasReservation)
                return ServiceResult.Error("Rezervasyon Hatası",
                    $"{request.ReservationDate.ToShortDateString()} tarihinde bu masa zaten rezerve edilmiş.", HttpStatusCode.BadRequest);
            
            // 2. Masayı bul ve durumunu güncelle
            var hasTable = await context.Tables.FirstOrDefaultAsync(x => x.Id == request.TableId, cancellationToken);

            if (hasTable == null)
                return ServiceResult<CreateReservationCommand>.Error("Masa Bulunamadı",
                    $"Seçilen masa (ID: {request.TableId}) sistemde kayıtlı değil.", HttpStatusCode.NotFound);

            var newReservation = mapper.Map<Reservation>(request);
            newReservation.Created = DateTime.Now;
            newReservation.CustomerFullName = identityService.UserName;
            newReservation.Id = NewId.NextSequentialGuid(); // index performance

            hasTable.Status = Tables.TableStatus.Occupied; // Masayı dolu olarak işaretle

            context.Reservations.Add(newReservation);
            context.Tables.Update(hasTable);

            await context.SaveChangesAsync(cancellationToken);

            // 5. Cache temizliği
            cacheService.Remove("tables");
            cacheService.Remove("reservations");

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
