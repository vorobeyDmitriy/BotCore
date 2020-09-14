using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Core.Test.DomainModels;

namespace BotCore.Core.Test.Interfaces
{
    public interface IBankService
    {
        Task<List<Currency>> GetAllCurrencies();
        Task<Currency> GetCurrency(string currencyName, DateTime? dateTime = null);
    }
}