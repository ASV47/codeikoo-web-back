using Academy.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
    public class SmtpEmailSender(IConfiguration _configuration, ILogger<SmtpEmailSender> _logger) : IEmailSender
    {
        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            var host = _configuration["Smtp:Host"] ?? throw new Exception("SMTP Host is missing.");
            var port = int.Parse(_configuration["Smtp:Port"] ?? "587");
            var user = _configuration["Smtp:User"] ?? throw new Exception("SMTP User is missing.");
            var pass = _configuration["Smtp:Pass"] ?? throw new Exception("SMTP Pass is missing.");
            var fromEmail = _configuration["Smtp:FromEmail"] ?? user;
            var fromName = _configuration["Smtp:FromName"] ?? "App";
            var enableSsl = bool.Parse(_configuration["Smtp:EnableSsl"] ?? "true");

            using var message = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            using var smtp = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(user, pass),
            };

            try
            {
                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {ToEmail}", toEmail);
                throw; // أو ارمي Exception من نوعك
            }
        }
    }
}
