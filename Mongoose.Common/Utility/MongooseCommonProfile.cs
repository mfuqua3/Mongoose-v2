using AutoMapper;
using GoogleCast;
using GoogleCast.Models.Media;
using Mongoose.Common.Models;

namespace Mongoose.Common.Utility
{
    public class MongooseCommonProfile:Profile
    {
        public MongooseCommonProfile()
        {
            CreateMap<IReceiver, CastReceiver>();
        }   
    }
}