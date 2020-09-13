using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Interfaces;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Test.Keyboards;

namespace BotCore.Viber.Test.Actions
{
    public class GetCurrencyRateAction : ViberAction
    {
        private readonly IBankService _bankService;

        public GetCurrencyRateAction(IMessageSender<ViberMessage> messageSender, IBankService bankService) : base(
            messageSender)
        {
            _bankService = bankService;
        }

        public override async Task ExecuteAsync(ViberCommand command)
        {
            var usd = await _bankService.GetCurrency("USD");
            var eur = await _bankService.GetCurrency("EUR");
            var rub = await _bankService.GetCurrency("RUB");

            await MessageSender.SendTextAsync(new ViberMessage
            {
                Receiver = command.Receiver,
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = usd + "\r\n" + eur + "\r\n" + rub + "\r\n",
                SenderDisplayName = "Qwe"
            });
        }
    }
}