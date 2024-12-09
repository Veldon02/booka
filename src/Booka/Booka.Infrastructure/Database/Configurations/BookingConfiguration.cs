using Booka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booka.Infrastructure.Database.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        // Columns
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookDate).HasColumnType("datetime");
        builder.Property(x => x.CheckInDate).HasColumnType("datetime");

        builder.Property(x => x.UpdateDate).HasColumnType("datetime");
        builder.Property(x => x.CreateDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

        // Relations
        builder.HasOne(x => x.User).WithMany(x => x.Bookings);

        builder.HasOne(x => x.Workplace);
    }
}