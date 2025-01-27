
using Booka.Core.Domain;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Repositories;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Booka.Infrastructure.Database.Repositories;

public class WorkplaceRepository : BaseRepository<Workplace, int>, IWorkplaceRepository
{
    public WorkplaceRepository(BookaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Workplace>> GetByWorkspace(int workspaceId)
    {
        var query = dbSet.Where(x => x.WorkspaceId == workspaceId);

        return await query.ToListAsync();
    }

    public async Task<Workplace> Add(Workplace entity)
    {

        try
        {
            return await base.Add(entity);
        }
        catch (UniqueConstraintException)
        {
            throw new InvalidParametersException("Number should be unique within workspace");
        }
    }
}