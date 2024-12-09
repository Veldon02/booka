using Booka.Domain.Interfaces.Repositories;
using Booka.Domain.Models;

namespace Booka.Infrastructure.Database.Repositories;

public class WorkplaceRepository : BaseRepository<Workplace, int>, IWorkplaceRepository
{
    public WorkplaceRepository(BookaDbContext dbContext) : base(dbContext)
    {
    }
}