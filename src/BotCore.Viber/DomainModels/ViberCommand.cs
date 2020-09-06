using BotCore.Core.DomainModels;

namespace BotCore.Viber.DomainModels
{
    public class ViberCommand : MessengerCommandBase
    {
        public string Receiver { get; set; }
        
        public ViberCommand(string commandName) : base(commandName)
        {
        }
    }
}