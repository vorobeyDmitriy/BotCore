namespace BotCore.Core.DataTransfer
{
    /// <summary>
    ///     Base class for all messages
    /// </summary>
    public abstract class MessageBase
    {
        public string Receiver { get; set; }
        public string Text { get; set; }
    }
}