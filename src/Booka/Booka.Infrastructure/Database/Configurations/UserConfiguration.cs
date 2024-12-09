using Booka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booka.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Columns
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).HasColumnType("nvarchar(50)"); //TODO: should be encrypted
        builder.Property(x => x.LastName).HasColumnType("nvarchar(50)"); //TODO: should be encrypted

        builder.Property(x => x.Email).HasColumnType("nvarchar(50)"); //TODO: should be encrypted
        builder.Property(x => x.Password).HasColumnType("nvarchar(50)"); //TODO: should be encrypted
        builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar(50)"); //TODO: should be encrypted

        builder.Property(x => x.UpdateDate).HasColumnType("datetime");
        builder.Property(x => x.CreateDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

        // Relations 
        builder.HasMany(x => x.Bookings)
               .WithOne(x => x.User)
               .HasForeignKey("UserId");

        // Indexes
        builder.HasIndex(x => x.Email);
    }
}