using BotCore.Core.DomainModels;

namespace BotCore.Telegram.DomainModels
{
    public class TelegramCommand : MessengerCommandBase
    {
        public long SenderId { get; set; }
        public TelegramCommand(string commandName) : base(commandName)
        {
        }
    }
}