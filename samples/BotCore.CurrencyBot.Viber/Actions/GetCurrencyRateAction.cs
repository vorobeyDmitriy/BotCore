using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Entities;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.DataTransfer;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Viber.CurrencyBot.Keyboards;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;

namespace BotCore.Viber.CurrencyBot.Actions
{
    public class GetCurrencyRateAction : ViberAction
    {
        private readonly ICurrencyService _currencyService;
        private readonly IMessageService _messageService;
        private readonly IUsersService _usersService;

        public GetCurrencyRateAction(IMessageSender<ViberMessage> messageSender, IMessageService messageService,
            IUsersService usersService, ICurrencyService currencyService)
            : base(messageSender)
        {
            _messageService = messageService;
            _usersService = usersService;
            _currencyService = currencyService;
        }

        public override async Task<OperationResult> ExecuteAsync(ViberCommand command)
        {
            var defaultCurrencies = await _usersService.GetUserDefaultCurrencies(command.Receiver);

            if (!defaultCurrencies.Any())
                defaultCurrencies = new List<Currency>
                {
                    new Currency {Abbreviation = "USD"},
                    new Currency {Abbreviation = "EUR"},
                    new Currency {Abbreviation = "RUB"}
                };

            var sb = new StringBuilder();

            foreach (var currency in defaultCurrencies)
            {
                var gain = await _currencyService.GetCurrencyRateGain(currency.Abbreviation, DateTime.UtcNow);
                sb.Append(_messageService.GetCurrencyRateMessageAsync(gain));
            }

            await MessageSender.SendTextAsync(new ViberMessage
            {
                Receiver = command.Receiver,
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = sb.ToString(),
                SenderDisplayName = "Qwe"
            });
        }
    }
}