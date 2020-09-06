namespace BotCore.Core.DataTransfer
{
    public abstract class MessageBase
    {
        public string Receiver { get; set; }
        public string Text { get; set; }
    }
}