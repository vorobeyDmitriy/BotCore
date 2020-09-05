namespace BotCore.Core.DataTransfer
{
    public abstract class Message
    {
        public string Receiver { get; set; }
        public string Text { get; set; }
    }
}