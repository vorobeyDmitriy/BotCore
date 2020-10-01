using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.DomainModels;
using BotCore.Core.CurrencyBot.Entities;
using BotCore.Core.CurrencyBot.Interfaces;

namespace BotCore.CurrencyBot.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        private const string RatesWithDateUrl = "https://www.nbrb.by/api/exrates/rates/{0}?parammode=2&ondate={1}";
        private const string GetAllCurrenciesUrl = "https://www.nbrb.by/api/exrates/currencies";
        private readonly IApiProvider _apiProvider;
        private readonly IAsyncRepository<Currency> _currencyRepository;
        private readonly IDbCacheService _dbCacheService;

        public CurrencyService(IAsyncRepository<Currency> currencyRepository,
            IApiProvider apiProvider,
            IDbCacheService dbCacheService)
        {
            _currencyRepository = currencyRepository;
            _apiProvider = apiProvider;
            _dbCacheService = dbCacheService;
        }

        public async Task<CurrencyModel> GetCurrency(string currencyAbbreviation, DateTime? dateTime = null)
        {
            dateTime = dateTime?.Date ?? DateTime.UtcNow.Date;

            var currencyFromDb = await _dbCacheService.GetCurrencyRateFromCacheAsync(currencyAbbreviation,
                dateTime.Value);

            if (currencyFromDb != null)
                return new CurrencyModel
                {
                    Abbreviation = currencyAbbreviation,
                    Date = dateTime.Value,
                    Name = currencyFromDb.From.Name,
                    OfficialRate = currencyFromDb.Rate,
                    Scale = currencyFromDb.From.Scale
                };


            var url = string.Format(RatesWithDateUrl, currencyAbbreviation, dateTime.Value.ToString("yyyy-MM-dd"));
            var currency = await _apiProvider.GetAsync<CurrencyModel>(url);

            if (currency == null)
                return null;

            await _dbCacheService.SetCurrencyRateToCacheAsync(currency, dateTime.Value);

            return currency;
        }

        public async Task<CurrencyGain> GetCurrencyRateGain(string currencyAbbreviation, DateTime? dateTime = null)
        {
            dateTime = dateTime?.Date ?? DateTime.UtcNow.Date;

            var next = await GetCurrency(currencyAbbreviation, dateTime.Value.AddDays(1));
            var curr = await GetCurrency(currencyAbbreviation, dateTime.Value);

            if (next == null && curr == null)
                return null;

            if (next == null)
            {
                next = curr;
                curr = await GetCurrency(currencyAbbreviation, dateTime.Value.AddDays(-1));
            }

            return new CurrencyGain
            {
                Abbreviation = curr.Abbreviation,
                Gain = next.OfficialRate - curr.OfficialRate,
                Scale = next.Scale,
                LatestRate = (decimal) next.OfficialRate,
                Date = next.Date
            };
        }

        public async Task<List<Currency>> GetAllCurrencies()
        {
            var currencies = await _currencyRepository.ListAllAsync();

            if (currencies.Any())
                return currencies.ToList();

            var result = await _apiProvider.GetAsync<List<CurrencyModel>>(GetAllCurrenciesUrl);

            return result
                .GroupBy(x => x.Abbreviation)
                .Select(x => x.FirstOrDefault())
                .Where(x => x != null)
                .Select(x => new Currency
                {
                    Abbreviation = x.Abbreviation,
                    Name = x.Name,
                    Rate = (decimal) x.OfficialRate,
                    Scale = x.Scale
                }).ToList();
        }

        public async Task<int> GetCurrenciesCountAsync()
        {
            var currencies = await _currencyRepository.ListAllAsync();

            return currencies.Count;
        }
    }
}