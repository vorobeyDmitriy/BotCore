using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BotCore.Core.Test.Constants;
using BotCore.Core.Test.DomainModels;
using BotCore.Core.Test.Entities;
using BotCore.Core.Test.Interfaces;

namespace BotCore.Core.Test.Services
{
    public class MessageService : IMessageService
    {
        private const string Plus = "+";
        private readonly ICurrencyService _currencyService;

        public MessageService(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public string GetAllCurrenciesMessageAsync(IEnumerable<Currency> currencies)
        {
            var sb = new StringBuilder();

            foreach (var currency in currencies)
            {
                sb.Append(currency.Name);
                sb.Append(" ( ");
                sb.Append(currency.Abbreviation);
                sb.Append(") ");
                sb.Append("\r\n");
            }

            return sb.ToString();
        }


        public string GetCurrencyRateMessageAsync(CurrencyGain gain)
        {
            if (gain == null)
                return MessagesConstants.CurrencyNotFound;
            
            var arrow = GetArrow(gain.Gain);
            
            var message = $"{arrow} {gain.Scale} {gain.Abbreviation} :  {gain.LatestRate} BYN " +
                          $"({(gain.Gain > 0 ? Plus : string.Empty)}{gain.Gain:F4}) \r\n";

            return message;
        }

        private static string GetArrow(double difference)
        {
            var arrow = MessagesConstants.ArrowRight;

            if (difference > 0)
                arrow = MessagesConstants.ArrowUp;

            if (difference < 0)
                arrow = MessagesConstants.ArrowDown;

            return arrow;
        }
    }
}