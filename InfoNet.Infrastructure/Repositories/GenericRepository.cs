using InfoNet.Domain.Entities;
using InfoNet.Domain.Repositories;
using InfoNet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfoNet.Infrastructure.Repositories;

public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
    protected readonly ApplicationDbContext Context;

    protected GenericRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    public virtual async Task<IQueryable<TEntity>> GetAllAsync()
    {
        return await Task.FromResult(Context.Set<TEntity>().AsQueryable());
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        return Task.FromResult(entity);
    }

    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        return await Task.FromResult(entity);
    }
}