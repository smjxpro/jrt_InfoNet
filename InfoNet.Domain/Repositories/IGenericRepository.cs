using InfoNet.Domain.Entities;

namespace InfoNet.Domain.Repositories;

public interface IGenericRepository<TEntity, in TId> where TEntity : BaseEntity<TId>
{
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}