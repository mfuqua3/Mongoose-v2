using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core.Repository.BaseTypes
{
    internal abstract class ModelRepository<TEntity> : BaseRepository<TEntity, int> where TEntity : class, IEntity<int>
    {
        protected ModelRepository(MongooseContext mongooseContext) : base(mongooseContext)
        {
        }
    }
}