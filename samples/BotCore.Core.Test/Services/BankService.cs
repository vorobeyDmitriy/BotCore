using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BotCore.Core.Test.Entities;
using BotCore.Core.Test.Interfaces;
using BotCore.Core.Test.Specifications;
using Newtonsoft.Json;
using Currency = BotCore.Core.Test.DomainModels.Currency;
using CurrencyEntity = BotCore.Core.Test.Entities.Currency;

namespace BotCore.Core.Test.Services
{
    public class BankService : IBankService
    {
        private const string Byn = "BYN";
        private const string RatesWithDateUrl = "https://www.nbrb.by/api/exrates/rates/{0}?parammode=2&ondate={1}";
        private const string GetAllCurrenciesUrl = "https://www.nbrb.by/api/exrates/currencies";
        private readonly IAsyncRepository<CurrencyRate> _currencyRateRepository;
        private readonly IAsyncRepository<CurrencyEntity> _currencyRepository;

        public BankService(IAsyncRepository<CurrencyRate> currencyRateRepository,
            IAsyncRepository<CurrencyEntity> currencyRepository)
        {
            _currencyRateRepository = currencyRateRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<Currency> GetCurrency(string currencyAbbreviation, DateTime? dateTime = null)
        {
            dateTime = dateTime?.Date ?? DateTime.UtcNow.Date;

            var currencyRateSpec = new CurrencyRateSpecification(currencyAbbreviation, dateTime.Value);
            var currencyFromDb = (await _currencyRateRepository.ListAsync(currencyRateSpec))
                .FirstOrDefault();

            if (currencyFromDb != null)
                return new Currency
                {
                    Abbreviation = currencyAbbreviation,
                    Date = dateTime.Value,
                    Name = currencyFromDb.From.Name,
                    OfficialRate = currencyFromDb.Rate,
                    Scale = currencyFromDb.From.Scale
                };

            using var client = GetClient();
            var url = string.Format(RatesWithDateUrl, currencyAbbreviation, dateTime.Value.ToString("yyyy-MM-dd"));
            var response = await client.GetAsync(url);
            var currency = await ProcessResponse<Currency>(response);

            var currencySpec = new CurrencySpecification(currencyAbbreviation);
            var from = (await _currencyRepository.ListAsync(currencySpec)).FirstOrDefault();
            currencySpec = new CurrencySpecification(Byn);
            var to = (await _currencyRepository.ListAsync(currencySpec)).FirstOrDefault();

            if (from == null || to == null)
                return currency;

            var entity = new CurrencyRate
            {
                Rate = currency.OfficialRate,
                Date = dateTime.Value.Date,
                FromId = from.Id,
                ToId = to.Id
            };
            await _currencyRateRepository.AddAsync(entity);

            return currency;
        }

        public async Task<List<Currency>> GetAllCurrencies()
        {
            using var client = GetClient();

            var response = await client.GetAsync(GetAllCurrenciesUrl);
            var currency = await ProcessResponse<List<Currency>>(response);

            return currency;
        }

        private static HttpClient GetClient()
        {
            return new HttpClient {Timeout = TimeSpan.FromSeconds(30)};
        }

        private static async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return default;

            var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}