using System.Net.Mail;

namespace LeaveManagmentSystem.Services.EmailService
{
    public class EmailSender(IConfiguration configuration) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var sender = configuration["EmailSettings:EmailAdress"];
            var smtp_server = configuration["EmailSettings:Server"];
            var smtp_port = Convert.ToInt32(configuration["EmailSettings:Port"]);

            var message = new MailMessage
            {
                From = new MailAddress(sender!),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true

            };

            message.To.Add(new MailAddress(email)); 
            using var client = new SmtpClient(smtp_server, smtp_port);

            await client.SendMailAsync(message);
            
        }
    }
}
