using BotCore.Core.DataTransfer;
using BotCore.Core.Interfaces;
using BotCore.Viber.DomainModels;

namespace BotCore.Viber.DataTransfer
{
    public abstract class ViberAction : ActionBase<ViberCommand>
    {
        protected readonly IMessageSender<ViberMessage> MessageSender;

        protected ViberAction(IMessageSender<ViberMessage> messageSender)
        {
            MessageSender = messageSender;
        }
    }
}