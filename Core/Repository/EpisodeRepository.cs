using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Core.Repository
{
    internal class EpisodeRepository : ModelRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(MongooseContext mongooseContext) : base(mongooseContext)
        {
        }

        protected override IQueryable<Episode> GetModifiedQuery(QueryInject<Episode> queryInject)
        {
            return base.GetModifiedQuery(queryInject).Include(e=>e.VideoInfo);
        }
    }
}