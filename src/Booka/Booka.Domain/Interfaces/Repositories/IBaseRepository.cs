namespace Booka.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity, TId>
    where TEntity : class 
{
    Task<TEntity?> GetById(TId id);

    Task<List<TEntity>> GetAll();

    Task<TEntity> Add(TEntity entity);

    Task Remove(TEntity entity);

    Task Update(TEntity entity);
}