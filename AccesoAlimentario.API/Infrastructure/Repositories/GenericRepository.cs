using System.Linq.Expressions;
using AccesoAlimentario.API.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace AccesoAlimentario.API.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                         new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
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
                // make sure to track the entity
                var obj = _context.Find(typeof(TEntity), id);
                _context.Update(obj);
                
                return obj as TEntity;
            }
            catch (Exception)
            {
                Console.WriteLine("Error al obtener el objeto por Id");
                return null;
            }
        }

        public virtual TEntity Insert(TEntity entity)
        {
            var obj = _dbSet.Add(entity);
            _context.SaveChanges();
            return obj.Entity;
        }

        public List<TEntity> InsertMany(IEnumerable<TEntity> entities)
        {
            var objs = new List<TEntity>();
            foreach (var entity in entities)
            {
                var obj = _dbSet.Add(entity);
                objs.Add(obj.Entity);
            }
            _context.SaveChanges();
            return objs;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
