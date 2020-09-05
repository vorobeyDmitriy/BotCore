using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Telegram.Test.DomainModels;

namespace BotCore.Telegram.Test.Interfaces
{
    public interface IBankService
    {
        Task<List<Currency>> GetAllCurrencies();
        Task<Currency> GetCurrency(string currencyName);
    }
}