using System.ComponentModel.DataAnnotations.Schema;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core.Entities
{
    public class Film : Entity<int>, IMediaInfo, IOrdered
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int Number { get; set; }

        [ForeignKey(nameof(VideoInfo))]
        public int VideoInfoId { get; set; }
        public VideoInfo VideoInfo { get; set; }
        [ForeignKey(nameof(Anthology))]
        public int? AnthologyId { get; set; }
        public FilmAnthology Anthology { get; set; }
    }
}