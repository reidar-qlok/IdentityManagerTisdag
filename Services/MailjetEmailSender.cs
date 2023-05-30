using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Net;

namespace IdentityManager.Services
{
    public class MailjetEmailSender : IEmailSender
    {
        public readonly IConfiguration _configuration;
        public MailJetOptions _mailJetOptions;
        public MailjetEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (MailMessage mail = new MailMessage())
            {
                _mailJetOptions = _configuration.GetSection("Google").Get<MailJetOptions>();
                mail.From = new MailAddress(_mailJetOptions.UserName);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = htmlMessage;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(_mailJetOptions.UserName, _mailJetOptions.AppPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}
