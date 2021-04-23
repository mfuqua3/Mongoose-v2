using System.ComponentModel.DataAnnotations;

namespace Mongoose.Api.Models.Series.Request
{
    public class UpdateSeriesRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
    }
}