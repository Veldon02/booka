using Booka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booka.Infrastructure.Database.Configurations;

public class WorkplaceConfiguration : IEntityTypeConfiguration<Workplace>
{
    public void Configure(EntityTypeBuilder<Workplace> builder)
    {
        // Columns
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number).HasColumnType("int");
        builder.Property(x => x.Type).HasColumnType("varchar(50)");

        builder.Property(x => x.UpdateDate).HasColumnType("datetime");
        builder.Property(x => x.CreateDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

        // Relations
        builder.HasOne(x => x.Workspace)
               .WithMany(x => x.Workplaces);
    }
}