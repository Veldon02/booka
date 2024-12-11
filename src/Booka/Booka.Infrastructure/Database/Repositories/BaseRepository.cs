using Booka.Domain.Interfaces.Repositories;
using Booka.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
}