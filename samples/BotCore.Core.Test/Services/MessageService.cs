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
        private readonly IAsyncRepository<Currency> _currencyRepository;
        private readonly IBankService _bankService;
        private const string Minus = "-";
        private const string Plus = "+";

        public MessageService(IAsyncRepository<Currency> currencyRepository, IBankService bankService)
        {
            _currencyRepository = currencyRepository;
            _bankService = bankService;
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

        public async Task<string> GetCurrencyRateMessageAsync(string currency)
        {
            var date = DateTime.UtcNow.AddDays(1);
            
            var usdNext = await _bankService.GetCurrency(currency, date);

            if (usdNext == null)
            {
                date = date.AddDays(-1);
                usdNext = await _bankService.GetCurrency(currency, date);
                date = date.AddDays(-1);
            }                

            var usdPrev = await _bankService.GetCurrency(currency, date);
            var diff = usdNext.OfficialRate - usdPrev.OfficialRate;

            var message = $"{usdNext.Scale} {usdNext.Abbreviation} :  {usdNext.OfficialRate}  BYN " +
                          $"({(diff>0 ? Plus : Minus)}{diff}) \r\n";

            return message;
        }

        public async Task<int> GetCurrenciesCountAsync()
        {
            var currencies = await _currencyRepository.ListAllAsync();

            return currencies.Count;
        }
    }
}