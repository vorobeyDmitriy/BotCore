using BotCore.Core.DomainModels;

namespace BotCore.Telegram.DomainModels
{
    public class TelegramCommand : MessengerCommandBase
    {
        public TelegramCommand(string commandName) : base(commandName)
        {
        }
    }
}