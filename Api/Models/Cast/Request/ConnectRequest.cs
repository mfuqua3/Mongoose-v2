using System.ComponentModel.DataAnnotations;

namespace Mongoose.Api.Models.Cast.Request
{
    public class ConnectRequest
    {
        [Required]
        public string ReceiverId { get; set; }
    }
}