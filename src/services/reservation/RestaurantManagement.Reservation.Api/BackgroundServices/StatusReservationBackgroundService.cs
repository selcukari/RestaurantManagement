using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.BackgroundServices
{
    public class StatusReservationBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // 1. Create the scope
                using (var scope = scopeFactory.CreateScope())
                {
                    // 2. Resolve scoped services from the scope's provider
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();

                    context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

                    var reservations = await context.Reservations
                        .Where(x => x.IsAvailable && x.ReservationDate < DateTime.UtcNow.Date)
                        .ToListAsync(stoppingToken);

                    if (reservations.Any())
                    {
                        foreach (var reservation in reservations)
                        {
                            var table = await context.Tables.FindAsync([reservation.TableId], stoppingToken);
                            if (table == null) continue;

                            reservation.IsAvailable = false;
                            table.Status = Features.Tables.TableStatus.Empty;
                        }

                        await context.SaveChangesAsync(stoppingToken);

                        // 3. Use the scoped cache service
                        cacheService.Remove("tables");
                        cacheService.Remove("reservations");
                    }
                }

                // Wait for 24 hours
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
