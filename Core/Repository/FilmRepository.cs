using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Core.Repository
{
    internal class FilmRepository:ModelRepository<Film>, IFilmRepository {
        public FilmRepository(MongooseContext mongooseContext) : base(mongooseContext)
        {
        }
    }
}