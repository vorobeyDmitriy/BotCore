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

        public MessageService(IAsyncRepository<Currency> currencyRepository)
        {
            _currencyRepository = currencyRepository;
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
        
        public async Task<int> GetCurrenciesCountAsync()
        {
            var currencies = await _currencyRepository.ListAllAsync();

            return currencies.Count;
        }
    }
}