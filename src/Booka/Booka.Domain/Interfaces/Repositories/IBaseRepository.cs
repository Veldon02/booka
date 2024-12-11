namespace Booka.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity, TId>
    where TEntity : class 
{
    Task<TEntity?> GetById(TId id, bool asNoTracking = true);

    Task<List<TEntity>> GetAll(bool asNoTracking = true);

    Task<TEntity> Add(TEntity entity);

    Task Remove(TEntity entity);

    Task Update(TEntity entity);
}