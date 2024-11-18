using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.Core.DAL;

public class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    

    public IQueryable<TEntity> GetQueryable()
    {
        return _dbSet;
    }
    
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var result = await _dbSet.FindAsync(id);
        
        return result;
    }

    public async Task<TEntity?> GetAsync(IQueryable<TEntity> query, bool track = true)
    {
        var result = track
            ? await query.FirstOrDefaultAsync()
            : await query.AsNoTracking().FirstOrDefaultAsync();
        
        return result;
    }

    public async Task<IEnumerable<TEntity>> GetCollectionAsync(IQueryable<TEntity> query, bool track = true)
    {
        var result = track
            ? await query.ToListAsync()
            : await query.AsNoTracking().ToListAsync();
        
        return result;
    }

    public Task<int> CountAsync(IQueryable<TEntity> query)
    {
        return query.AsNoTracking().CountAsync();
    }

    public Task<bool> ExistsAsync(Guid uuid)
    {
        return Task.FromResult(_dbSet.Find(uuid) != null);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TEntity entityToUpdate)
    {
        _dbSet.Update(entityToUpdate);
        return Task.CompletedTask;
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entitiesToUpdate)
    {
        _dbSet.UpdateRange(entitiesToUpdate);
        return Task.CompletedTask;
    }
}