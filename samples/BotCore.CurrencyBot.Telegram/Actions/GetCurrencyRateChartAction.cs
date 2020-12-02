using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Constants;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.CurrencyBot.Keyboards;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.CurrencyBot.Actions
{
    public class GetCurrencyRateChartAction : TelegramAction
    {
        private readonly IPlotService _plotService;
        private readonly ICurrencyService _currencyService;
        private readonly IUsersService _usersService;

        public GetCurrencyRateChartAction(IMessageSender<TelegramMessage> messageSender, IPlotService plotService,
            ICurrencyService currencyService, IUsersService usersService)
            : base(messageSender)
        {
            _plotService = plotService;
            _currencyService = currencyService;
            _usersService = usersService;
        }

        public override async Task<OperationResult> ExecuteAsync(TelegramCommand command)
        {
            if (IsFirstStepMessage(command.Text, ActionConstants.GetCurrencyRateChart))
                return await SendReply(command);

            var months = Convert.ToInt32(command.Text, CultureInfo.InvariantCulture);
            var startDate = DateTime.UtcNow.AddMonths(-1 * months);
            var endDate = DateTime.UtcNow;

            var defaultCurrencies = await _usersService.GetUserDefaultCurrencies(command.SenderUsername);

            var results = new List<OperationResult>();

            foreach (var currency in defaultCurrencies)
            {
                var currencies = await GetCurrencies(startDate, endDate, currency.Abbreviation);
                var days = GetDates(startDate, endDate, currencies.Count);
                var picturePath = _plotService.SavePlot(days, currencies);

                var result = await MessageSender.SendPictureAsync(new TelegramMessage
                {
                    Receiver = command.ChatId.ToString(),
                    Keyboard = GetCurrencyRateKeyboard.Keyboard,
                    Text = currency.Abbreviation
                }, picturePath);

                results.Add(result);
                _plotService.DeletePlot(picturePath);
            }

            return results.FirstOrDefault(x => !x.Success) ?? results.FirstOrDefault();
        }

        private async Task<OperationResult> SendReply(TelegramCommand commandBase)
        {
            return await MessageSender.SendTextAsync(
                       new TelegramMessage
                       {
                           Keyboard = new ForceReplyMarkup(),
                           Text = $"{ActionConstants.GetCurrencyRateChart}{MessagesConstants.NextLine}" +
                                  $"{MessagesConstants.SelectDateInterval}",
                           Receiver = commandBase.ChatId.ToString(),
                           ReplyToMessageId = commandBase.MessageId
                       });
        }

        private async Task<List<double>> GetCurrencies(DateTime start, DateTime end, string currencyAbbreviation)
        {
            var currencies = (await _currencyService.GetCurrencyRatesInInterval(currencyAbbreviation, start, end))
                             .Select(x => x.OfficialRate)
                             .ToList();

            return currencies;
        }

        private static IEnumerable<double> GetDates(DateTime start, DateTime end, int expectedCount)
        {
            var days = new List<double>();

            for (var i = start; i < end; i = i.AddDays(1))
            {
                days.Add(i.Date.ToOADate());
            }

            while (days.Count != expectedCount)
            {
                days.RemoveAt(0);
            }

            return days;
        }
    }
}