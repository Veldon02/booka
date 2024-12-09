using Booka.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Booka.Infrastructure.Database.Repositories;

public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
{
    private BookaDbContext _dbContext;
    protected DbSet<TEntity> dbSet;

    public BaseRepository(BookaDbContext dbContext)
    {
        _dbContext = dbContext;
        dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetById(TId id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await dbSet.ToListAsync();
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