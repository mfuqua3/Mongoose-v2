using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core.Repository.BaseTypes
{
    internal abstract class BaseRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId :  IEquatable<TId>
    {
        protected readonly MongooseContext MongooseContext;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(MongooseContext mongooseContext)
        {
            MongooseContext = mongooseContext;
            DbSet = mongooseContext.Set<TEntity>();
        }

        public async Task<bool> Contains(TId id)
        {
            return await DbSet.AnyAsync(e => e.Id.Equals(id));
        }

        public async Task<bool> Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }

        public async Task<int> Count()
        {
            return await DbSet.CountAsync();
        }

        public virtual async Task<TEntity> Get(TId id, QueryInject<TEntity> queryInject = null)
        {
            return await GetModifiedQuery(queryInject).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual async Task<TEntity> GetFull(TId id) => await Get(id);

        public virtual async Task<List<TEntity>> GetAll(QueryInject<TEntity> queryInject = null)
        {
            return await GetModifiedQuery(queryInject).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null) return await GetAll();
            return await GetAll(qry => qry.Where(predicate));
        }

        public virtual async Task<List<TEntity>> GetWhereFull(Expression<Func<TEntity, bool>> predicate = null)
            => await GetWhere(predicate);
        public virtual Task Post(TEntity entity)
        {
            DbSet.Add(entity);
            return Task.CompletedTask;
        }

        public virtual async Task Put(TEntity entity)
        {
            var existingEntity = await Get(entity.Id);
            if (existingEntity == null)
                throw new InvalidOperationException(
                    "Invalid PUT operation, no entity exists in the database with the specified ID.");
            MongooseContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public virtual async Task<TEntity> Delete(TId id)
        {
            var existingEntity = await Get(id);
            if (existingEntity == null)
                throw new InvalidOperationException(
                    "Invalid DELETE operation, no entity exists in the database with the specified ID.");
            DbSet.Remove(existingEntity);
            return existingEntity;
        }

        public async Task Save()
        {
            await MongooseContext.SaveChangesAsync();
        }

        protected virtual IQueryable<TEntity> GetModifiedQuery(QueryInject<TEntity> queryInject)
        {
            return queryInject != null ? queryInject(DbSet) : DbSet;
        }
    }
}