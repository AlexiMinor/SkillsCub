using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SkillsCub.EmailSenderService
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {
            //TODO get from settings
            _emailSettings = new EmailSettings()
            {
                PrimaryDomain = "smtp.gmail.com",
                PrimaryPort = 587,
                UsernameEmail = "SkillsCub@gmail.com",
                UsernamePassword = "80173190299",
                FromEmail = "SkillsCub"
            };
        }

        private readonly EmailSettings _emailSettings;

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Execute(email, subject, message).Wait();
                return Task.FromResult(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task Execute(string email, string subject, string message)
        {
            try
            {
                var toEmail = string.IsNullOrEmpty(email)
                    ? _emailSettings.ToEmail
                    : email;

                var mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail)
                };
                mail.To.Add(new MailAddress(toEmail));
                if (!string.IsNullOrEmpty(_emailSettings.CcEmail))
                {
                    mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                }

                mail.Subject = "SkillsCub - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (var smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}