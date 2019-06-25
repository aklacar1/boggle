using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = message;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("", "");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            return Task.CompletedTask;
        }
    }
}