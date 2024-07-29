using System.Linq.Expressions;

namespace AccesoAlimentario.Core.DAL;

public interface IRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    TEntity? GetById(object id);

    TEntity Insert(TEntity entity);
    List<TEntity> InsertMany(IEnumerable<TEntity> entities);

    void Delete(TEntity entityToDelete);

    void Update(TEntity entityToUpdate);
}