using Booka.Domain.Interfaces.Repositories;
using Booka.Domain.Models;

namespace Booka.Infrastructure.Database.Repositories;

public class BookingRepository : BaseRepository<Booking, int>, IBookingRepository
{
    public BookingRepository(BookaDbContext dbContext) : base(dbContext)
    {
    }
}