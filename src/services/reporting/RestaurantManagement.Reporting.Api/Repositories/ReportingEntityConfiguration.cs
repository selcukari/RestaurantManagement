using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace RestaurantManagement.Reporting.Api.Repositories
{
    public class ReportingEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<RestaurantManagement.Reporting.Api.Features.Reporting.Reporting> builder)
        {
            builder.ToCollection("reportings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Update).HasElementName("update");

            // Listeyi MongoDB içindeki bir array (döküman listesi) olarak mapliyoruz
            builder.OwnsMany(x => x.ReservationRepors, r =>
            {
                r.Property(x => x.Id).HasElementName("id");
                r.Property(x => x.CustomerFullName).HasElementName("customerFullName");
                r.Property(x => x.TableId).HasElementName("tableId");
                r.Property(x => x.CustomerId).HasElementName("customerId");
                r.Property(x => x.ReservationDate).HasElementName("reservationDate");
                r.Property(x => x.StartTime).HasElementName("startTime");
                r.Property(x => x.EndTime).HasElementName("endTime");
                r.Property(x => x.GuestCount).HasElementName("guestCount");
            });
        }
    }
}
