using System.Threading.Tasks;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Interfaces;
using BotCore.Telegram.Test.Interfaces;
using BotCore.Telegram.Test.Keyboards;

namespace BotCore.Telegram.Test.Actions
{
    public class GetCurrencyRateAction : TelegramAction
    {
        private readonly IBankService _bankService;

        public GetCurrencyRateAction(IMessageSender<TelegramMessage> messageSender, IBankService bankService) : base(messageSender)
        {
            _bankService = bankService;
        }

        public override async Task ExecuteAsync(TelegramCommand command)
        {
            var usd = await _bankService.GetCurrency("USD");
            var eur = await _bankService.GetCurrency("EUR");
            var rub = await _bankService.GetCurrency("RUB");

            await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.SenderId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = usd + "\r\n" + eur + "\r\n" + rub + "\r\n"
            });
        }
    }
}