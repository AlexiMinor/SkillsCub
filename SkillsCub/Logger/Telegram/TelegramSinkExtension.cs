using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Telegram.Bot;

namespace SkillsCub.Logger.Telegram
{
    public static class TelegramSinkExtension
    {
        public static LoggerConfiguration Telegram(
            this LoggerSinkConfiguration loggerConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
            ITelegramBotClient botClient = null,
            IConfiguration configuration = null
        )
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException(paramName: nameof(loggerConfiguration));

            return loggerConfiguration.Sink(
                logEventSink: (ILogEventSink)new TelegramSink(formatProvider: formatProvider, botClient: botClient, configuration: configuration
                ),
                restrictedToMinimumLevel: restrictedToMinimumLevel);
        }
    }
}
