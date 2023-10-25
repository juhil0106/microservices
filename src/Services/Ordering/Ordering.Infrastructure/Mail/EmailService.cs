using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Ordering.Application.Contracts.Infrastucture;
using Ordering.Application.Models;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_emailSettings.FromEmail));
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.Subject = email.Subject;
            mail.Body = new BodyBuilder() { TextBody = email.Body }.ToMessageBody();

            var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.Host, Convert.ToInt32(_emailSettings.Port), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);

            return true;
        }
    }
}
