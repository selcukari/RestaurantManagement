using System.Reflection;
using MongoDB.Driver;
using RestaurantManagement.Menu.Api.Features.Menus;
using RestaurantManagement.Menu.Api.Features.Products;

namespace RestaurantManagement.Menu.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Menum> Menus { get; set; }


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
