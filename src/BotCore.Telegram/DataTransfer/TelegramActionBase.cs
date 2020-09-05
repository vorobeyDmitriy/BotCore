using BotCore.Core.DataTransfer;
using BotCore.Core.Interfaces;

namespace BotCore.Telegram.DataTransfer
{
    public abstract class TelegramActionBase : ActionBase
    {
        protected readonly IMessageSender MessageSender;

        protected TelegramActionBase(IMessageSender messageSender)
        {
            MessageSender = messageSender;
        }
    }
}