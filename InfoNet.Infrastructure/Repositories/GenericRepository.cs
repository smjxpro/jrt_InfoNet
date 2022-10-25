using InfoNet.Domain.Entities;
using InfoNet.Domain.Repositories;
using InfoNet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfoNet.Infrastructure.Repositories;

public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        return await Task.FromResult(_context.Set<TEntity>().AsQueryable());
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return Task.FromResult(entity);
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return await Task.FromResult(entity);
    }
}