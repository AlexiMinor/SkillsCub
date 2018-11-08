using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace Logger.Telegram
{
    static class TelegramSinkExtension
    {
        public static LoggerConfiguration Telegram(
            this LoggerSinkConfiguration loggerConfiguration,
            string token,
            string chatId,
            TelegramSink.RenderMessageMethod renderMessageImplementation = null,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null
        )
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException(paramName: nameof(loggerConfiguration));

            return loggerConfiguration.Sink(
                logEventSink: (ILogEventSink)new TelegramSink(
                    chatId: chatId,
                    token: token,
                    renderMessageImplementation: renderMessageImplementation,
                    formatProvider: formatProvider
                ),
                restrictedToMinimumLevel: restrictedToMinimumLevel);
        }
    }
}
