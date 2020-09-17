using System;
using System.Threading.Tasks;

namespace BotCore.Core.Test.Interfaces
{
    public interface IMessageService
    {
        /// <summary>
        /// Get paginated string that contains currencies
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// For example:
        ///     U.S. Dollar (USD) \r\n Belarusian Rubel (BYN)
        /// </returns>
        Task<string> GetAllCurrenciesMessageAsync(int pageNumber, int pageSize);
        /// <summary>
        /// Get currency rate message
        /// </summary>
        /// <param name="currency">Currency abbreviation</param>
        /// <param name="date">Date</param>
        /// <returns>
        /// For example:
        ///     1 USD: 5 BYN (+4.0505)
        /// </returns>
        Task<string> GetCurrencyRateMessageAsync(string currency, DateTime date);
    }
}