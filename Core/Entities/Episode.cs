using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core.Entities
{
    [Index(nameof(SeasonId), nameof(Number), IsUnique = true, Name = "IX_EpisodeNumber")]
    public class Episode : Entity<int>, IMediaInfo, IOrdered
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int Number { get; set; }
        [ForeignKey(nameof(VideoInfo))]
        public int VideoInfoId { get; set; }
        public VideoInfo VideoInfo { get; set; }
        [ForeignKey(nameof(Season))]
        public int SeasonId { get; set; }
        public Season Season { get; set; }
        [NotMapped]
        public int? SeasonNumber => Season?.Number;
    }
}