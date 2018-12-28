using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        //Synchronous Methods
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        //Asynchronous Methods
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        //Add Remove Methods
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
