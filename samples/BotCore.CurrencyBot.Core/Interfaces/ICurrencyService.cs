using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.DomainModels;
using BotCore.Core.CurrencyBot.Entities;

namespace BotCore.Core.CurrencyBot.Interfaces
{
    public interface ICurrencyService
    {
        /// <summary>
        ///     Get list of all currencies
        /// </summary>
        /// <returns></returns>
        Task<List<Currency>> GetAllCurrencies();

        /// <summary>
        ///     Get concrete currency
        /// </summary>
        /// <param name="currencyAbbreviation">Currency abbreviation</param>
        /// <param name="dateTime">Date</param>
        /// <returns></returns>
        Task<CurrencyModel> GetCurrency(string currencyAbbreviation, DateTime? dateTime = null);

        /// <summary>
        ///     Get concrete currency rate gain comparing with previous date
        /// </summary>
        /// <param name="currencyAbbreviation">Currency abbreviation</param>
        /// <param name="dateTime">Date</param>
        /// <returns></returns>
        Task<CurrencyGain> GetCurrencyRateGain(string currencyAbbreviation, DateTime? dateTime = null);

        /// <summary>
        ///     Get currencies count
        /// </summary>
        /// <returns></returns>
        Task<int> GetCurrenciesCountAsync();
    }
}