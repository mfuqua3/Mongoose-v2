using System.ComponentModel.DataAnnotations;

namespace Mongoose.Api.Models.Cast.Request
{
    public class LoadRequest
    {
        [Required]
        public string ReceiverId { get; set; }
        [Required]
        public int ContentId { get; set; }
    }
}