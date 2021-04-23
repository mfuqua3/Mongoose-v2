using System.ComponentModel.DataAnnotations;

namespace Mongoose.Api.Models.Season.Request
{
    public class UpdateSeasonRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }
    }
}