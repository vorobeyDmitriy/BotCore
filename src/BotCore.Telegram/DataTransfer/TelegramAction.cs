using BotCore.Core.DataTransfer;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;

namespace BotCore.Telegram.DataTransfer
{
    public abstract class TelegramAction : ActionBase<TelegramCommand>
    {
        protected readonly IMessageSender<TelegramMessage> MessageSender;

        protected TelegramAction(IMessageSender<TelegramMessage> messageSender)
        {
            MessageSender = messageSender;
        }
    }
}