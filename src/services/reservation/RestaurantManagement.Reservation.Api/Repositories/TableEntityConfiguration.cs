using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using RestaurantManagement.Reservation.Api.Features.Tables;

namespace RestaurantManagement.Reservation.Api.Repositories
{
    public class TableEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            // Koleksiyon adı
            builder.ToCollection("tables"); // Genelde varlık adıyla uyumlu olması tercih edilir (reservations yerine tables)

            // Primary Key yapılandırması
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            // Özellik yapılandırmaları
            builder.Property(x => x.TableNumber)
                   .HasElementName("tableNumber")
                   .IsRequired();

            builder.Property(x => x.UserFullName)
                   .HasElementName("userFullName")
                   .HasMaxLength(150); // İsim soyisim için 150 daha güvenli bir sınır olabilir

            builder.Property(x => x.Capacity)
                   .HasElementName("capacity")
                   .IsRequired();

            builder.Property(x => x.Created)
                   .HasElementName("created")
                   .IsRequired();

            builder.Property(x => x.IsAvailable)
                   .HasElementName("isAvailable");

            // Enum Yapılandırmaları
            // Veritabanında string olarak saklamak sorgulanabilirliği artırır (Window, VIP vb.)
            // Eğer sayı olarak saklamak isterseniz .HasConversion kısmını kaldırabilirsiniz.
            builder.Property(x => x.Location)
                   .HasElementName("location")
                   .HasConversion<string>();

            builder.Property(x => x.Status)
                   .HasElementName("status")
                   .HasConversion<string>();
        }
    }
}
