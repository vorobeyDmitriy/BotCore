using System.Threading.Tasks;
using BotCore.Core.DataTransfer;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Keyboards;

namespace BotCore.Telegram.Test.Actions
{
    public class StartAction: ActionBase
    {
        private readonly IMessageSender _messageSender;

        public StartAction(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public override async Task ExecuteAsync(MessengerCommandBase commandBase)
        {
            if(!(commandBase is TelegramCommand command))
                return;

            await _messageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.SenderId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = "Hi!"
            });
        }
    }
}