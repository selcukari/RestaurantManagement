using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace RestaurantManagement.Reservation.Api.Repositories
{
    public class ReservationEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<RestaurantManagement.Reservation.Api.Features.Reservations.Reservation> builder)
        {
            // Koleksiyon adı
            builder.ToCollection("reservations"); // Genelde varlık adıyla uyumlu olması tercih edilir (reservations yerine tables)
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            // Tarih: Sadece Gün bazlı
            builder.Property(x => x.ReservationDate)
                   .HasElementName("reservationDate")
                   .IsRequired();

            // TimeSpan Yapılandırması
            // MongoDB sağlayıcısı TimeSpan'i genellikle "Ticks" veya "String" olarak saklar.
            builder.Property(x => x.StartTime)
                   .HasElementName("startTime")
                   .IsRequired();

            builder.Property(x => x.EndTime)
                   .HasElementName("endTime")
                   .IsRequired();

            builder.Property(x => x.Created)
                   .HasElementName("created")
                   .IsRequired();

            builder.Property(x => x.TableId).HasElementName("tableId");
            builder.Property(x => x.CustomerId).HasElementName("customerId");
            builder.Property(x => x.GuestCount).HasElementName("guestCount");
        }
    }
}
