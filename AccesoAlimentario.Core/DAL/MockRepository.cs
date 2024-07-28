using System.Linq.Expressions;
using AccesoAlimentario.Core.DAL;

public class MockRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly List<TEntity> _data = new List<TEntity>();

    public IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _data.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return query.ToList();
    }

    public TEntity? GetById(object id)
    {
        return _data.FirstOrDefault();
    }

    public void Insert(TEntity entity)
    {
        _data.Add(entity);
    }

    public void InsertMany(IEnumerable<TEntity> entities)
    {
        _data.AddRange(entities);
    }

    public void Delete(TEntity entityToDelete)
    {
        _data.Remove(entityToDelete);
    }

    public void Update(TEntity entityToUpdate)
    {
        var existingEntity = _data.FirstOrDefault(e => e.Equals(entityToUpdate));
        if (existingEntity != null)
        {
            _data.Remove(existingEntity);
            _data.Add(entityToUpdate);
        }
    }
}