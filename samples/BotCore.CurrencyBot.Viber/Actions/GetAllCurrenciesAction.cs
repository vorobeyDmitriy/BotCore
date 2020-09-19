using System;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Viber.CurrencyBot.Keyboards;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;

namespace BotCore.Viber.CurrencyBot.Actions
{
    public class GetAllCurrenciesAction : ViberAction
    {
        private const int PageSize = 8;
        private readonly ICurrencyService _currencyService;
        private readonly IMessageService _messageService;

        public GetAllCurrenciesAction(IMessageSender<ViberMessage> messageSender, IMessageService messageService,
            ICurrencyService currencyService)
            : base(messageSender)
        {
            _messageService = messageService;
            _currencyService = currencyService;
        }

        public override async Task<OperationResult> ExecuteAsync(ViberCommand command)
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