using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DnevnaDoza.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpHost = "smtp.mailtrap.io"; // Mailtrap SMTP server
        private readonly int _smtpPort = 587; // Standardni SMTP port za Mailtrap
        private readonly string _smtpUser = "b1b59e6fac2c20"; 
        private readonly string _smtpPass = "ad0ab9f9868def";

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_smtpHost, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("zpandza1@etf.unsa.ba", "Dnevna Doza"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}