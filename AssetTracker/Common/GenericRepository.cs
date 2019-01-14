using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly IDbContext _context;

        public GenericRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //Synchronous Methods
        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        protected IQueryable<TEntity> FindIf(bool condition, Expression<Func<TEntity, bool>> predicate)
        {
            return condition == true ? _context.Set<TEntity>().FindIf(condition, predicate) : _context.Set<TEntity>();
        }


        //Asynchronous Methods
        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        //Add and Remove Methods
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
