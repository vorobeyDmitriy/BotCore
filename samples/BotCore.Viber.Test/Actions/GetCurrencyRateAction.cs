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
        private readonly IMessageService _messageService;
        
        public GetCurrencyRateAction(IMessageSender<ViberMessage> messageSender, IMessageService messageService) 
            : base(messageSender)
        {
            _messageService = messageService;
        }

        public override async Task ExecuteAsync(ViberCommand command)
        {
            var usd = await _messageService.GetCurrencyRateMessageAsync("USD");
            var eur = await _messageService.GetCurrencyRateMessageAsync("EUR");
            var rub = await _messageService.GetCurrencyRateMessageAsync("RUB");

            await MessageSender.SendTextAsync(new ViberMessage
            {
                Receiver = command.Receiver,
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = usd + eur + rub,
                SenderDisplayName = "Qwe"
            });
        }
    }
}