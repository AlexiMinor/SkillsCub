using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SkillsCub.TelegramLogger
{
    public class TelegramLogger : ITelegramLogger
    {
        public readonly ITelegramBotClient BotClient;

        public TelegramLogger()
        {
            BotClient = new TelegramBotClient("532273025:AAEaFQYGW5L3dTRmrCo9_10V2UahXyqf_S0");
        }

        public async Task<Message> Trace(string message)
        {
            var messageText = $"[TRACE] {DateTime.Now:G} {Environment.NewLine} {message}";
            return await BotClient.SendTextMessageAsync("-255366715", messageText, ParseMode.Markdown);

        }
        public async Task<Message> Debug(string message)
        {
            var messageText = $"[DEBUG] {DateTime.Now:G} {Environment.NewLine} {message}";
            return await BotClient.SendTextMessageAsync("-255366715", messageText, ParseMode.Markdown);

        }
        public async Task<Message> Info(string message)
        {
            var messageText = $"[INFO] {DateTime.Now:G} {Environment.NewLine} {message}";
            return await BotClient.SendTextMessageAsync("-255366715", messageText, ParseMode.Markdown);
        }
        public async Task<Message> Warning(string message)
        {
            var messageText = $"[WARNING] {DateTime.Now:G} {Environment.NewLine} {message}";
            return await BotClient.SendTextMessageAsync("-255366715", messageText, ParseMode.Markdown);

        }
        public async Task<Message> Error(string message)
        {
            var messageText = $"[ERROR] {DateTime.Now:G} {Environment.NewLine} {message}";
            return await BotClient.SendTextMessageAsync("-255366715", messageText, ParseMode.Markdown);
        }
    }
}
