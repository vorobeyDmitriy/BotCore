using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Common.Test.DomainModels;

namespace BotCore.Common.Test.Interfaces
{
    public interface IBankService
    {
        Task<List<Currency>> GetAllCurrencies();
        Task<Currency> GetCurrency(string currencyName);
    }
}