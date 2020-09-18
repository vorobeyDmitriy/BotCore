﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Entities;
using BotCore.Core.Test.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Keyboards;

namespace BotCore.Telegram.Test.Actions
{
    public class GetCurrencyRateAction : TelegramAction
    {
        private readonly IMessageService _messageService;
        private readonly IUsersService _usersService;
        private readonly ICurrencyService _currencyService;

        public GetCurrencyRateAction(IMessageSender<TelegramMessage> messageSender, IMessageService messageService,
            IUsersService usersService, ICurrencyService currencyService)
            : base(messageSender)
        {
            _messageService = messageService;
            _usersService = usersService;
            _currencyService = currencyService;
        }

        public override async Task ExecuteAsync(TelegramCommand command)
        {
            var defaultCurrencies = await _usersService.GetUserDefaultCurrencies(command.SenderUsername);

            if (!defaultCurrencies.Any())
                defaultCurrencies = new List<Currency>
                {
                    new Currency {Abbreviation = "USD"},
                    new Currency {Abbreviation = "EUR"},
                    new Currency {Abbreviation = "RUB"},
                };

            var sb = new StringBuilder();

            foreach (var currency in defaultCurrencies)
            {
                var gain = await _currencyService.GetCurrencyRateGain(currency.Abbreviation, DateTime.UtcNow);
                sb.Append(_messageService.GetCurrencyRateMessageAsync(gain));
            }

            await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.ChatId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = sb.ToString()
            });
        }
    }
}