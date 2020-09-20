using System;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.DomainModels;
using BotCore.Core.CurrencyBot.Entities;

namespace BotCore.Core.CurrencyBot.Interfaces
{
    public interface IDbCacheService
    {
        Task<CurrencyRate> GetCurrencyRateFromCacheAsync(string currencyAbbreviation, DateTime dateTime);

        Task SetCurrencyRateToCacheAsync(CurrencyModel currencyModel, DateTime dateTime);
    }
}