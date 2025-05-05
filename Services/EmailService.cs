using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace MapTask.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string toEmail, string subject, string htmlBody)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    _config["EmailSettings:Username"],
                    _config["EmailSettings:Password"]
                )
            };

            var message = new MailMessage
            {
                From = new MailAddress
                (
                    _config["EmailSettings:FromAddress"],
                    _config["EmailSettings:DisplayName"]
                ),

                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            smtp.Send(message);
        }
    }
}
