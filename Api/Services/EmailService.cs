using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mongoose.Api.Configuration;
using Mongoose.Api.Services.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Mongoose.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridClient _sendGridClient;
        private readonly ApplicationOptions _applicationConfig;
        private readonly SendGridOptions _sendGridConfig;

        public EmailService(IOptions<SendGridOptions> sendGridOptions, IOptions<ApplicationOptions> applicationOptions)
        {
            _sendGridClient = new SendGridClient(sendGridOptions.Value.ApiKey);
            _applicationConfig = applicationOptions.Value;
            _sendGridConfig = sendGridOptions.Value;
        }

        public string CreateResetEmailBody(string resetToken, string userId)
        {
            var resetUrl = $"{_applicationConfig.ApplicationRoot}/resetpassword/{userId}/{resetToken}";
            return $"<div>\r" +
                   $"<h2>{_applicationConfig.ApplicationName} Password Reset Request</h2>\r" +
                   $"<p>A request has been made to reset the password linked to this email. " +
                   $"To complete this request, please follow the link provided below.</p>" +
                   $"<a href=\"{resetUrl}\">{resetUrl}</a>\r" +
                   $"<hr />" +
                   $"<p>If this request was made in error, please ignore this message. " +
                   $"The link provided will expire in 24 hours.</p>\r" +
                   $"</div>";
        }

        public async Task<HttpStatusCode> SendEmail(string to, string subject, string htmlContent)
        {
            var fromAddress = new EmailAddress(_sendGridConfig.FromAddress);
            var toAddress = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, "", htmlContent);
            var response = await _sendGridClient.SendEmailAsync(message);
            return response.StatusCode;
        }
    }
}