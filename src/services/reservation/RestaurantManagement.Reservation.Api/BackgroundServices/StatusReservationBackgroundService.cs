using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.BackgroundServices
{
    public class StatusReservationBackgroundService(AppDbContext context, ICacheService cacheService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

            // iptal request gelmedigi surece bu dongu devam edecek
            while (!stoppingToken.IsCancellationRequested)
            {
                var reservations = await context.Reservations.Where(x => x.IsAvailable && x.ReservationDate < DateTime.UtcNow.Date)
                .ToListAsync(stoppingToken);

                foreach (var reservation in reservations)
                {
                    var table = await context.Tables.FindAsync([reservation.TableId], stoppingToken);
                    if (table == null) continue;

                    reservation.IsAvailable = false;
                    table.Status = Features.Tables.TableStatus.Empty;

                    context.Tables.Update(table);
                    context.Reservations.Update(reservation);
                }

                await context.SaveChangesAsync(stoppingToken);

                // Cache temizliği
                cacheService.Remove("tables");
                cacheService.Remove("reservations");

                await Task.Delay(24 * 60 * 60 * 1000, stoppingToken); // 1 gün bekleme
            }
        }
    }
}
