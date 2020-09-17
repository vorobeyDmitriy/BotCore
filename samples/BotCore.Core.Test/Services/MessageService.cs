using System;
using System.Text;
using System.Threading.Tasks;
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

            var nextDate = date.AddDays(1);

            var usdNext = await _currencyService.GetCurrency(currency, nextDate);

            if (usdNext == null) return await GetCurrencyRateMessageAsync(currency, date.AddDays(-1));

            var usdPrev = await _currencyService.GetCurrency(currency, date);
            var diff = usdNext.OfficialRate - usdPrev.OfficialRate;

            var message = $"{usdNext.Scale} {usdNext.Abbreviation} :  {usdNext.OfficialRate} BYN " +
                          $"({(diff > 0 ? Plus : string.Empty)}{diff:F4}) \r\n";

            return message;
        }
    }
}