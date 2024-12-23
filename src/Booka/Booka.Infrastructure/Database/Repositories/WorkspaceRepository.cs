using Booka.Core.Domain;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Security;
using Microsoft.EntityFrameworkCore;

namespace Booka.Infrastructure.Database.Repositories;

public class WorkspaceRepository : BaseRepository<Workspace, int>, IWorkspaceRepository
{
    private readonly IHasher _hasher;

    public WorkspaceRepository(BookaDbContext dbContext, IHasher hasher)
        : base(dbContext)
    {
        _hasher = hasher;
    }

    public async Task<Workspace?> GetByEmail(string email, bool asNoTracking = true)
    {
        var query = asNoTracking ? dbSet.AsNoTracking() : dbSet;

        return await query.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<Workspace> Add(Workspace entity)
    {
        entity.Password = _hasher.Hash(entity.Password);

        return await base.Add(entity);
    }
}