using Booka.Domain.Interfaces.Repositories;
using Booka.Domain.Models;

namespace Booka.Infrastructure.Database.Repositories;

public class WorkspaceRepository : BaseRepository<Workspace, int>, IWorkspaceRepository
{
    public WorkspaceRepository(BookaDbContext dbContext)
        : base(dbContext)
    {
    }
}