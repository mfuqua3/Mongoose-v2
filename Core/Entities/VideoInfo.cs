using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core.Entities
{
    [Index(nameof(FilePath), IsUnique = true)]
    public class VideoInfo : Entity<int>, IVideoInfo, INamed
    {

        public string FilePath { get; set; }
        public long Duration { get; set; }
        [ForeignKey(nameof(Episode))]
        public int? EpisodeId { get; set; }
        public Episode Episode { get; set; }
        [ForeignKey(nameof(Film))]
        public int? FilmId { get; set; }
        public Film Film { get; set; }
        public string Name { get; set; }
    }
}