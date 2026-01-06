using System.Reflection;
using MongoDB.Driver;

namespace RestaurantManagement.Kitchen.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<RestaurantManagement.Kitchen.Api.Features.Kitchens.Kitchen> Kitchens { get; set; }


        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client,
                    database.DatabaseNamespace.DatabaseName);


            return new AppDbContext(optionsBuilder.Options);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
