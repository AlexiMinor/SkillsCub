using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Serilog.Events;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SkillsCub.Logger.Telegram
{
    public class TelegramSink : ILogEventSink
    {
        /// <summary>
        /// Delegate to allow overriding of the RenderMessage method.
        /// </summary>

        protected readonly IFormatProvider FormatProvider;
        private readonly ITelegramBotClient _botClient;
        private readonly IConfiguration _configuration;


        public TelegramSink(IFormatProvider formatProvider, ITelegramBotClient botClient, IConfiguration configuration)
        {
            FormatProvider = formatProvider;
            _botClient = botClient;
            _configuration = configuration;
        }

        #region ILogEventSink implementation

        public async void Emit(LogEvent logEvent)
        {
            var message = FormatProvider != null
                ? logEvent.RenderMessage(formatProvider: FormatProvider)
                : RenderMessage(logEvent);

           await SendMessage(message);
        }

        #endregion

        protected static string RenderMessage(LogEvent logEvent)
        {
            var sb = new StringBuilder();
            sb.AppendLine(value: $"{GetEmoji(log: logEvent)} {logEvent.RenderMessage()}");

            if (logEvent.Exception != null)
            {
                sb.AppendLine(value: $"\n*{logEvent.Exception.Message}*\n");
                sb.AppendLine(value: $"Message: `{logEvent.Exception.Message}`");
                sb.AppendLine(value: $"Type: `{logEvent.Exception.GetType().Name}`\n");
                sb.AppendLine(value: $"Stack Trace\n```{logEvent.Exception}```");
            }

            return sb.ToString();
        }

        private static string GetEmoji(LogEvent log)
        {
            switch (log.Level)
            {
                case LogEventLevel.Debug:
                    return "👉";
                case LogEventLevel.Error:
                    return "❗";
                case LogEventLevel.Fatal:
                    return "‼";
                case LogEventLevel.Information:
                    return "ℹ";
                case LogEventLevel.Verbose:
                    return "⚡";
                case LogEventLevel.Warning:
                    return "⚠";
                default:
                    return "";
            }
        }

        private async Task<Message> SendMessage(string message) 
            => await _botClient.SendTextMessageAsync(_configuration["Teleram:ChatId"], message, ParseMode.Markdown);
    }
}
