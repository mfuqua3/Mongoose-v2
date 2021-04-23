using System.ComponentModel.DataAnnotations;

namespace Mongoose.Api.Models.Episode.Request
{
    public class UpdateEpisodeRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int? Number { get; set; }
    }
}