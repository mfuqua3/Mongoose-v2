using System.ComponentModel.DataAnnotations;

namespace Mongoose.Api.Models.Episode.Request
{
    public class CreateEpisodeRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }
        [Required]
        public int VideoInfoId { get; set; }
        [Required]
        public int SeasonId { get; set; }
    }
}