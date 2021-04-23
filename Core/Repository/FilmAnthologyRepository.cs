using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Core.Repository
{
    internal class FilmAnthologyRepository : ModelRepository<FilmAnthology>, IFilmAnthologyRepository
    {
        public FilmAnthologyRepository(MongooseContext mongooseContext) : base(mongooseContext)
        {
        }
    }
}

    