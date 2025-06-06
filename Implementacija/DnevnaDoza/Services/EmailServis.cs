using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace DnevnaDoza.Services
{
    public class EmailServis
    {
        private readonly IConfiguration _configuration;

        public EmailServis(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Kreiranje e-mail poruke
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(
                _configuration["EmailSettings:SenderName"],
                _configuration["EmailSettings:SenderEmail"]
            ));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body // HTML sadržaj e-maila
            };

            email.Body = bodyBuilder.ToMessageBody();

            // Slanje putem SMTP servera
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _configuration["EmailSettings:SMTPServer"],
                int.Parse(_configuration["EmailSettings:Port"]),
                MailKit.Security.SecureSocketOptions.StartTls
            );
            await smtp.AuthenticateAsync(
                _configuration["EmailSettings:SenderEmail"],
                _configuration["EmailSettings:Password"]
            );
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}