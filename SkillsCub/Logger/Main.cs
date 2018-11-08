using System;
using Logger.Telegram;
using Serilog;

namespace Logger
{
    public class Main
    {
        public static void TelegramMain()
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Telegram("000000:AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "000000")
                .CreateLogger();

            try
            {
                log.Verbose("This is an verbose message!");
                log.Debug("This is an debug message!");
                log.Information("This is an information message!");
                log.Warning("This is an warning message!");
                log.Error("This is an error message!");
                throw new Exception("This is an exception!");
            }
            catch (Exception exception)
            {
                log.Fatal(exception, "Exception catched at Main.");
            }
        }
    }
}
