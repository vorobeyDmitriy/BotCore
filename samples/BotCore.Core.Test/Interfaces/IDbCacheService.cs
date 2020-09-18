using System;
using System.Threading.Tasks;
using BotCore.Core.Test.DomainModels;
using BotCore.Core.Test.Entities;

namespace BotCore.Core.Test.Interfaces
{
    public interface IDbCacheService
    {
        Task<CurrencyRate> GetCurrencyRateFromCacheAsync(string currencyAbbreviation, DateTime dateTime);

        Task SetCurrencyRateToCacheAsync(CurrencyModel currencyModel, DateTime dateTime);
    }
}