using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace DnevnaDoza.Services
{



    public class EmailServis : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailServis> _logger;

        public EmailServis(IConfiguration configuration, ILogger<EmailServis> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        //Sumeja dodala konstuktro bez loggera 
        public EmailServis(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public Task SendEmailAsync(string email, string subject, string htmlMessage)
       // {
            // Implement the method logic here
          //  return Task.CompletedTask;
        //}
        //ovdje zavrsava ta izmjena 
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(
                    _configuration["EmailSettings:SenderName"],
                    _configuration["EmailSettings:SenderEmail"]
                ));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body
                };

                email.Body = bodyBuilder.ToMessageBody();

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                // Opcionalno: Za debugovanje, isključite validaciju sertifikata (NE preporučuje se u produkciji)
                // smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await smtp.ConnectAsync(
                    _configuration["EmailSettings:SMTPServer"],
                    int.Parse(_configuration["EmailSettings:Port"]),
                    MailKit.Security.SecureSocketOptions.StartTls
                );

                await smtp.AuthenticateAsync(
                    _configuration["EmailSettings:SMTPUser"], // Koristi SMTPUser umesto SenderEmail
                    _configuration["EmailSettings:SMTPPass"]  // Koristi SMTPPass umesto Password
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                _logger.LogInformation($"Email poslat na {toEmail}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Greška prilikom slanja emaila na {toEmail}: {ex.Message}");
                throw;
            }
        }
    }
}