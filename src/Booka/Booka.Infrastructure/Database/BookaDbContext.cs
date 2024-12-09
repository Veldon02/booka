using Booka.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booka.Infrastructure.Database;

public class BookaDbContext : DbContext
{
    public BookaDbContext(DbContextOptions<BookaDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Workspace> Workspaces { get; set; }

    public DbSet<Workplace> Workplaces { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookaDbContext).Assembly);
    }
}
