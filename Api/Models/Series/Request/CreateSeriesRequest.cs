using System.ComponentModel.DataAnnotations;

namespace Mongoose.Api.Models.Series.Request
{
    public class CreateSeriesRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}