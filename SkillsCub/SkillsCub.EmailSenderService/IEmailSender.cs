using System.Threading.Tasks;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.EmailSenderService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}