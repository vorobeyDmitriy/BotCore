using Viber.Bot;
using MessageBase = BotCore.Core.DataTransfer.MessageBase;

namespace BotCore.Viber.DomainModels
{
    public class ViberMessage : MessageBase
    {
        public string SenderDisplayName { get; set; }
        public Keyboard Keyboard { get; set; }
    }
}