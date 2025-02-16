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
}