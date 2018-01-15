using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SkillsCub.TelegramLogger
{
    public interface ITelegramLogger
    {
        Task<Message> Error(string message);
        Task<Message> Warning(string message);
        Task<Message> Info(string message);
        Task<Message> Debug(string message);
        Task<Message> Trace(string message);
    }
}