using System.Threading.Tasks;
using BotCore.Core.DataTransfer;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Interfaces;
using BotCore.Telegram.Test.Keyboards;

namespace BotCore.Telegram.Test.Actions
{
    public class StartAction : TelegramAction
    {
        public StartAction(IMessageSender<TelegramMessage> messageSender) : base(messageSender)
        {
        }

        public override async Task ExecuteAsync(TelegramCommand command)
        {
            await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.SenderId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = "Hi!"
            });
        }
    }
}