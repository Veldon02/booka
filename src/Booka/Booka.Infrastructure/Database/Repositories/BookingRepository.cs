using Booka.Core.Domain;
using Booka.Core.DTOs.Common;
using Booka.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Booka.Infrastructure.Database.Repositories;

public class BookingRepository : BaseRepository<Booking, int>, IBookingRepository
{
    public BookingRepository(BookaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Booking>> GetByUser(int userId)
    {
        var query = dbSet.AsNoTracking();

        query = query
            .Include(x => x.Workplace)
            .Where(x => x.UserId == userId);

        return await query.ToListAsync();
    }

    public async Task<List<Booking>> GetByUserAndWorkplace(int userId, int workplaceId)
    {
        var query = dbSet.Where(x => x.UserId == userId && x.WorkplaceId == workplaceId);

        // For now we do this filtering here, to avoid fetching old bookings.
        // This should be implemented on DB lvl to cleanup such bookings 
        query = query.Where(x => x.BookDate > DateTime.UtcNow.AddDays(-1));

        return await query.ToListAsync();
    }
}