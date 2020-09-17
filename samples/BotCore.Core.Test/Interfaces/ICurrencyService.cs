using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Core.Test.DomainModels;

namespace BotCore.Core.Test.Interfaces
{
    public interface ICurrencyService
    {
        /// <summary>
        /// Get list of all currencies
        /// </summary>
        /// <returns></returns>
        Task<List<Currency>> GetAllCurrencies();
        
        /// <summary>
        /// Get concrete currency
        /// </summary>
        /// <param name="currencyAbbreviation">Currency abbreviation</param>
        /// <param name="dateTime">Date</param>
        /// <returns></returns>
        Task<Currency> GetCurrency(string currencyAbbreviation, DateTime? dateTime = null);
        
        /// <summary>
        /// Get currencies count
        /// </summary>
        /// <returns></returns>
        Task<int> GetCurrenciesCountAsync();
    }
}