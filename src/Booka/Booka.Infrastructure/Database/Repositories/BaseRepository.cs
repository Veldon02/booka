using Booka.Core.Domain;
using Booka.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Booka.Infrastructure.Database.Repositories;

public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
{
    private BookaDbContext _dbContext;
    protected DbSet<TEntity> dbSet;

    public BaseRepository(BookaDbContext dbContext)
    {
        _dbContext = dbContext;
        dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetById(TId id, bool asNoTracking = true)
    {
        var query = asNoTracking ? dbSet.AsNoTracking() : dbSet;
        return await query.FirstOrDefaultAsync(x => Equals(x.Id, id));
    }

    public async Task<List<TEntity>> GetAll(bool asNoTracking = true)
    {
        var query = asNoTracking ? dbSet.AsNoTracking() : dbSet;
        return await query.ToListAsync();
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        dbSet.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Remove(TEntity entity)
    {
        dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    protected static IQueryable<TEntity> ApplyPagination(IQueryable<TEntity> query, int page, int pageSize)
    {
        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }

    protected static async Task<(List<TEntity>, int)> FetchWithPagination(IQueryable<TEntity> query, int page, int pageSize)
    {
        var totalCount = await query.CountAsync();

        query = ApplyPagination(query, page, pageSize);

        var items = await query.ToListAsync();

        return (items, totalCount);
    }
}