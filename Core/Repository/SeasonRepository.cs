using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Core.Repository
{
    internal class SeasonRepository : ModelRepository<Season>, ISeasonRepository
    {
        public SeasonRepository(MongooseContext mongooseContext) : base(mongooseContext)
        {
        }

        public override async Task<Season> GetFull(int id)
        {
            return await Get(id, qry =>
                qry.Include(s => s.Episodes)
                    .ThenInclude(e => e.VideoInfo));
        }

        public override async Task<List<Season>> GetWhereFull(Expression<Func<Season, bool>> predicate = null)
        {
            if (predicate == null)
                return await GetAll(qry => qry.Include(s => s.Episodes)
                    .ThenInclude(e => e.VideoInfo));
            return await GetAll(qry => qry.Include(s => s.Episodes)
                .ThenInclude(e => e.VideoInfo).Where(predicate));
        }
    }
}