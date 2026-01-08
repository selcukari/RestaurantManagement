using Microsoft.EntityFrameworkCore;

namespace RestaurantManagement.Reservation.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
    }
}
