using System;
using System.Collections.Generic;
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

namespace BotCore.Telegram.CurrencyBot.Actions
{
    public class GetCurrencyRateChartAction: TelegramAction
    {

        private readonly IPlotService _plotService;
        private readonly ICurrencyService _currencyService;
        
        public GetCurrencyRateChartAction(IMessageSender<TelegramMessage> messageSender, IPlotService plotService, ICurrencyService currencyService)
            : base(messageSender)
        {
            _plotService = plotService;
            _currencyService = currencyService;
        }

        public override async Task<OperationResult> ExecuteAsync(TelegramCommand command)
        {
            var startDate = DateTime.UtcNow.AddDays(-365);
            var endDate = DateTime.UtcNow;
            var currencies = await GetCurrencies(startDate, endDate, "USD");
            var days = GetDates(startDate, endDate, currencies.Count);
            var picturePath = _plotService.SavePlot(days, currencies);
            
            var result = await MessageSender.SendPictureAsync(new TelegramMessage
            {
                Receiver = command.ChatId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = $"{MessagesConstants.Hello}"
            }, picturePath);
            _plotService.DeletePlot(picturePath);

            return result;
        }

        private async Task<List<double>> GetCurrencies(DateTime start, DateTime end, string currencyAbbreviation)
        {
            var currencies = (await _currencyService.GetCurrencyRatesInInterval(currencyAbbreviation, start, end))
                .Select(x => x.OfficialRate)
                .ToList();

            return currencies;
        }

        private static List<double> GetDates(DateTime start, DateTime end, int expectedCount)
        {
            var days = new List<double>();
            for (var i = start; i < end; i = i.AddDays(1))
            {
                days.Add(i.Date.ToOADate());
            }
            if(days.Count != expectedCount)
                days.RemoveAt(0);
            
            return days.ToList();
        }
    }
}