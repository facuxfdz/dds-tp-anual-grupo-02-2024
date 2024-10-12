namespace AccesoAlimentario.Core.DAL;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetQueryable();
    
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity> GetAsync(IQueryable<TEntity> query, bool track = true);
    Task<IEnumerable<TEntity>> GetCollectionAsync(IQueryable<TEntity> query, bool track = true);
    
    Task<int> CountAsync(IQueryable<TEntity> query);
    Task<bool> ExistsAsync(Guid uuid);
    
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    
    Task RemoveAsync(TEntity entity);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities);
    
    Task UpdateAsync(TEntity entityToUpdate);
    Task UpdateRangeAsync(IEnumerable<TEntity> entitiesToUpdate);
}