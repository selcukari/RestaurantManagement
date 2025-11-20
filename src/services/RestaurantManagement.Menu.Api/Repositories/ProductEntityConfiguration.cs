using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using RestaurantManagement.Menu.Api.Features.Products;

namespace RestaurantManagement.Menu.Api.Repositories
{
    public class ProductEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToCollection("products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(x => x.Created).HasElementName("created");
            builder.Property(x => x.UserId).HasElementName("userId");
            builder.Property(x => x.ImageUrl).HasElementName("imageUrl").HasMaxLength(200);
            builder.Property(x => x.MenuId).HasElementName("menuId");
            builder.Ignore(x => x.Menum);

            // id si olmayan field
            builder.OwnsOne(c => c.Feature, feature =>
            {
                feature.HasElementName("feature");
                feature.Property(x => x.Duration).HasElementName("duration");
                feature.Property(x => x.Rating).HasElementName("rating");
                feature.Property(x => x.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(100);
            });
        }
    }
}
