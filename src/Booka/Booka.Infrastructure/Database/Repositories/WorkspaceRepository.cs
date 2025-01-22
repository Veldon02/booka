using System.ComponentModel;
using Booka.Core.Domain;
using Booka.Core.Domain.enums.Workspace;
using Booka.Core.DTOs.Workspace;
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

    public async Task<(List<Workspace>, int)> Get(WorkspaceFilteringParams filter, WorkspaceSorting sort)
    {
        var query = dbSet.AsNoTracking();

        if (!string.IsNullOrEmpty(filter.Search))
        {
            query = query.Where(x => x.Name.Contains(filter.Search));
        }

        query = sort switch
        {
            WorkspaceSorting.NAME_ASC => query.OrderBy(x => x.Name),
            WorkspaceSorting.NAME_DESC => query.OrderByDescending(x => x.Name),
            _ => throw new InvalidEnumArgumentException($"Sorting {sort.ToString()} is not supported")
        };

        var totalCount = await query.CountAsync();

        query = ApplyPagination(query, filter.Page, filter.PageSize);

        var items = await query.ToListAsync();

        return (items, totalCount);
    }

    public async Task<Workspace> Add(Workspace entity)
    {
        entity.Password = _hasher.Hash(entity.Password);

        return await base.Add(entity);
    }
}