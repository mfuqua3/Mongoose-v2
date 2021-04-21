using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mongoose.Core.Entities;

namespace Mongoose.Core.Repository
{
    public interface IRepository<TEntity, in TId>
        where TEntity : IEntity<TId>
        where TId : IEquatable<TId>
    {
        Task<bool> Contains(TId id);
        Task<int> Count();
        Task<TEntity> Get(TId id, QueryInject<TEntity> queryInject = null);
        Task<List<TEntity>> GetAll(QueryInject<TEntity> queryInject = null);
        Task Post(TEntity entity);
        Task Put(TEntity entity);
        Task<TEntity> Delete(TId id);
        Task Save();
    }

    public delegate IQueryable<T> QueryInject<T>(IQueryable<T> query);
}