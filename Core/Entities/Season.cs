using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core.Entities
{
    [Index(nameof(SeriesId), nameof(Number), IsUnique = true, Name = "IX_SeasonNumber")]
    public class Season : Entity<int>, IMediaInfo, IOrdered
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int Number { get; set; }
        public List<Episode> Episodes { get; set; }
        [ForeignKey(nameof(Series))]
        public int SeriesId { get; set; }
        public Series Series { get; set; }
    }
}