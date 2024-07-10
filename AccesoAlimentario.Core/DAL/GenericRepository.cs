using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.Core.DAL;
public class GenericRepository<TEntity> where TEntity : class
{
    private readonly AppContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppContext context)
    {
        this._context = context;
        this._dbSet = context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where((Expression<Func<TEntity, bool>>)filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public virtual TEntity? GetById(object id)
    {
        try
        {
            var obj = _dbSet.Find(id);
            return obj ?? null;
        }
        catch (Exception)
        {
            Console.WriteLine("Error al obtener el objeto por Id");
            return null;
        }
    }

    public virtual void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        TEntity entityToDelete = _dbSet.Find(id);
        if (entityToDelete != null) Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}