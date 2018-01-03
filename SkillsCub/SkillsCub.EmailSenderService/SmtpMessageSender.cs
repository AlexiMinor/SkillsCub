using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SkillsCub.EmailSenderService
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            EmailSettings = emailSettings.Value;
        }

        public EmailSettings EmailSettings { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {

            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }
      
        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                var toEmail = string.IsNullOrEmpty(email)
                    ? EmailSettings.ToEmail
                    : email;

                var mail = new MailMessage()
                {
                    From = new MailAddress(EmailSettings.UsernameEmail)
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(EmailSettings.CcEmail));

                mail.Subject = "SkillsCub - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (var smtp = new SmtpClient(EmailSettings.SecondayDomain, EmailSettings.SecondaryPort))
                {
                    smtp.Credentials = new NetworkCredential(EmailSettings.UsernameEmail, EmailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                //todo something here
            }
        }
    }
}