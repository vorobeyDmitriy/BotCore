using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Keyboards;

namespace BotCore.Telegram.Test.Actions
{
    public class GetCurrencyRateAction : TelegramAction
    {
        private readonly IMessageService _messageService;

        public GetCurrencyRateAction(IMessageSender<TelegramMessage> messageSender, IMessageService messageService) 
            : base(messageSender)
        {
            _messageService = messageService;
        }

        public override async Task ExecuteAsync(TelegramCommand command)
        {
            var usd = await _messageService.GetCurrencyRateMessageAsync("USD");
            var eur = await _messageService.GetCurrencyRateMessageAsync("EUR");
            var rub = await _messageService.GetCurrencyRateMessageAsync("RUB");

            await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.SenderId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = usd + eur + rub
            });
        }
    }
}