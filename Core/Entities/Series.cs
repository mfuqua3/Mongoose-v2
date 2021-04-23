using System.Collections.Generic;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core.Entities
{
    public class Series : Entity<int>, IMediaInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public List<Season> Seasons { get; set; }
    }
}