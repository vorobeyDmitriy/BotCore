using System.Collections.Generic;
using System.Text;
using BotCore.Core.CurrencyBot.Constants;
using BotCore.Core.CurrencyBot.DomainModels;
using BotCore.Core.CurrencyBot.Entities;
using BotCore.Core.CurrencyBot.Interfaces;

namespace BotCore.CurrencyBot.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private const string Plus = "+";

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