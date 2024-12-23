
using Booka.Core.Domain;
using Booka.Core.Interfaces.Repositories;

namespace Booka.Infrastructure.Database.Repositories;

public class BookingRepository : BaseRepository<Booking, int>, IBookingRepository
{
    public BookingRepository(BookaDbContext dbContext) : base(dbContext)
    {
    }
}