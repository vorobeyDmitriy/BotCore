using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Entities;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.CurrencyBot.Keyboards;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;

namespace BotCore.Telegram.CurrencyBot.Actions
{
    public class GetCurrencyRateAction : TelegramAction
    {
        private readonly ICurrencyService _currencyService;
        private readonly IMessageService _messageService;
        private readonly IUsersService _usersService;

        public GetCurrencyRateAction(IMessageSender<TelegramMessage> messageSender, IMessageService messageService,
            IUsersService usersService, ICurrencyService currencyService)
            : base(messageSender)
        {
            _messageService = messageService;
            _usersService = usersService;
            _currencyService = currencyService;
        }

        public override async Task<OperationResult> ExecuteAsync(TelegramCommand command)
        {
            var defaultCurrencies = await _usersService.GetUserDefaultCurrencies(command.SenderUsername);

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

            return await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.ChatId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = sb.ToString()
            });
        }
    }
}