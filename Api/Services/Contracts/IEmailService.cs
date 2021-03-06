using System.Net;
using System.Threading.Tasks;

namespace Mongoose.Api.Services.Contracts
{
    public interface IEmailService
    {
        string CreateResetEmailBody(string resetToken, string userId);
        Task<HttpStatusCode> SendEmail(string to, string subject, string htmlContent);
    }
}