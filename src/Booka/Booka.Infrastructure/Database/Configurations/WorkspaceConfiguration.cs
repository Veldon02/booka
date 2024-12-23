using Booka.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booka.Infrastructure.Database.Configurations;

public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        // Columns
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnType("nvarchar(50)");
        builder.Property(x => x.Address).HasColumnType("nvarchar(50)");

        builder.Property(x => x.Email).HasColumnType("nvarchar(50)"); //TODO: should be encrypted
        builder.Property(x => x.Password).HasColumnType("nvarchar(max)");

        builder.Property(x => x.UpdateDate).HasColumnType("datetime");
        builder.Property(x => x.CreateDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

        // Relations
        builder.HasMany(x => x.Workplaces)
               .WithOne(x => x.Workspace)
               .HasForeignKey("WorkspaceId");

        // Indexes
        builder.HasIndex(x => x.Name);
    }
}