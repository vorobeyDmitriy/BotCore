using BotCore.Core.DomainModels;

namespace BotCore.Viber.DomainModels
{
    public class ViberCommand : MessengerCommandBase
    {
        public ViberCommand(string commandName) : base(commandName)
        {
        }
    }
}