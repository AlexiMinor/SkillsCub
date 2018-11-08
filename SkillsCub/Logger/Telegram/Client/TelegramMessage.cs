namespace Logger.Telegram.Client
{
    public class TelegramMessage
    {
        public TelegramMessage(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
