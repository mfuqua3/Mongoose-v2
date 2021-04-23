using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Core.Repository
{
    internal class VideoInfoRepository : ModelRepository<VideoInfo>, IVideoInfoRepository
    {
        public VideoInfoRepository(MongooseContext mongooseContext) : base(mongooseContext)
        {
        }
    }
}