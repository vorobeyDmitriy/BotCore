using System;
using System.Text;
using System.Threading.Tasks;
using BotCore.Core.Test.Constants;
using BotCore.Core.Test.Entities;
using BotCore.Core.Test.Interfaces;
using BotCore.Core.Test.Specifications;

namespace BotCore.Core.Test.Services
{
    public class MessageService : IMessageService
    {
        private const string Plus = "+";
        private readonly ICurrencyService _currencyService;
        private readonly IAsyncRepository<Currency> _currencyRepository;

        public MessageService(IAsyncRepository<Currency> currencyRepository, ICurrencyService currencyService)
        {
            _currencyRepository = currencyRepository;
            _currencyService = currencyService;
        }

        public async Task<string> GetAllCurrenciesMessageAsync(int pageNumber, int pageSize)
        {
            var spec = new CurrencySpecification(pageNumber, pageSize);
            var currencies = await _currencyRepository.ListAsync(spec);

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

        public async Task<string> GetCurrencyRateMessageAsync(string currency, DateTime date)
        {
            currency = currency.ToUpper();


            var usdNext = await _currencyService.GetCurrency(currency, date.AddDays(1));
            var usdCurrent = await _currencyService.GetCurrency(currency, date);

            if (usdNext == null)
                usdNext = usdCurrent;

            if (usdCurrent == null)
                return MessagesConstants.CurrencyNotFound;
            
            usdCurrent = await _currencyService.GetCurrency(currency, date.AddDays(-1));
            var diff = usdNext.OfficialRate - usdCurrent.OfficialRate;
            var arrow = GetArrow(diff);
            
            var message = $"{arrow} {usdNext.Scale} {usdNext.Abbreviation} :  {usdNext.OfficialRate} BYN " +
                          $"({(diff > 0 ? Plus : string.Empty)}{diff:F4}) \r\n";

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