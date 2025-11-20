using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using RestaurantManagement.Menu.Api.Features.Menus;

namespace RestaurantManagement.Menu.Api.Repositories
{
    public class MenuEntityConfiguration: IEntityTypeConfiguration<Menum>
    {
        public void Configure(EntityTypeBuilder<Menum> builder)
        {
            builder.ToCollection("menus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Ignore(x => x.Products);
        }
    }
}
