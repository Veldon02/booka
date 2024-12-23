
using Booka.Core.Domain;
using Booka.Core.Interfaces.Repositories;

namespace Booka.Infrastructure.Database.Repositories;

public class WorkplaceRepository : BaseRepository<Workplace, int>, IWorkplaceRepository
{
    public WorkplaceRepository(BookaDbContext dbContext) : base(dbContext)
    {
    }
}