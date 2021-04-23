using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Core.Repository
{
    internal class SeriesRepository : ModelRepository<Series>, ISeriesRepository
    {
        public SeriesRepository(MongooseContext mongooseContext) : base(mongooseContext)
        {
        }

        public override Task<Series> GetFull(int id)
        {
            return Get(id, qry =>
                qry.Include(s => s.Seasons)
                    .ThenInclude(s => s.Episodes)
                    .ThenInclude(e => e.VideoInfo));
        }
    }
}