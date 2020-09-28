using System.Collections.Generic;
using BotCore.Core.CurrencyBot.DomainModels;
using BotCore.Core.CurrencyBot.Entities;

namespace BotCore.Core.CurrencyBot.Interfaces
{
    public interface IMessageService
    {
        /// <summary>
        ///     Get paginated string that contains currencies
        /// </summary>
        /// <param name="currencies"></param>
        /// <returns>
        ///     For example:
        ///     U.S. Dollar (USD) \r\n Belarusian Rubel (BYN)
        /// </returns>
        string GetAllCurrenciesMessageAsync(IEnumerable<Currency> currencies);

        /// <summary>
        ///     Get currency rate gain message
        /// </summary>
        /// <param name="gain"></param>
        /// <returns>
        ///     For example:
        ///     1 USD: 5 BYN (+4.0505)
        /// </returns>
        string GetCurrencyRateGainMessageAsync(CurrencyGain gain);

        /// <summary>
        ///     Get currency rate message
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <returns>
        ///     For example:
        ///     1000 USD - 50000 BYN 
        /// </returns>
        string GetCurrencyRateMessageAsync(CurrencyModel currency, double amount);
    }
}