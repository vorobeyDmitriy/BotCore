using BotCore.Core.DomainModels;

namespace BotCore.Telegram.DomainModels
{
    public class TelegramCommand : MessengerCommandBase
    {
        public TelegramCommand(string commandName) : base(commandName)
        {
        }

        public long ChatId { get; set; }
        public int UserId { get; set; }
        public string SenderUsername { get; set; }
        public string Text { get; set; }
        public int MessageId { get; set; }
    }
}