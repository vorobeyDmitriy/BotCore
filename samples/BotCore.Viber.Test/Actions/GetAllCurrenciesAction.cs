using System;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Interfaces;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Test.Keyboards;

namespace BotCore.Viber.Test.Actions
{
    public class GetAllCurrenciesAction : ViberAction
    {
        private const int PageSize = 8;
        private readonly IMessageService _messageService;
        private readonly ICurrencyService _currencyService;

        public GetAllCurrenciesAction(IMessageSender<ViberMessage> messageSender, IMessageService messageService, 
            ICurrencyService currencyService)
            : base(messageSender)
        {
            _messageService = messageService;
            _currencyService = currencyService;
        }

        public override async Task ExecuteAsync(ViberCommand command)
        {
            var count = await _currencyService.GetCurrenciesCountAsync();
            var currencies = await _currencyService.GetAllCurrencies();
            var pageCount = Math.Ceiling((double) count / PageSize);
            for (var i = 0; i < pageCount; i++)
            {
                var message = _messageService.GetAllCurrenciesMessageAsync(currencies);
                await MessageSender.SendTextAsync(new ViberMessage
                {
                    Receiver = command.Receiver,
                    Keyboard = GetCurrencyRateKeyboard.Keyboard,
                    Text = message,
                    SenderDisplayName = "Qwe"
                });
            }
        }
    }
}