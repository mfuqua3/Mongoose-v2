using System.ComponentModel.DataAnnotations;

namespace Api.Models.Account.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ResetToken { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}