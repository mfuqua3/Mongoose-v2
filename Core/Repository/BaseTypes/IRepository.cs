using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core.Repository.BaseTypes
{
    public interface IRepository<TEntity, in TId>
        where TEntity : IEntity<TId>
        where TId : IEquatable<TId>
    {
        Task<bool> Contains(TId id);
        Task<bool> Contains(Expression<Func<TEntity, bool>> predicate);
        Task<int> Count();
        Task<TEntity> Get(TId id, QueryInject<TEntity> queryInject = null);
        Task<List<TEntity>> GetAll(QueryInject<TEntity> queryInject = null);
        Task Post(TEntity entity);
        Task Put(TEntity entity);
        Task<TEntity> Delete(TId id);
        Task Save();
        Task<TEntity> GetFull(TId id);
        Task<List<TEntity>> GetWhereFull(Expression<Func<TEntity, bool>> predicate = null);
        Task<List<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate = null);
    }

    public delegate IQueryable<T> QueryInject<T>(IQueryable<T> query);
}