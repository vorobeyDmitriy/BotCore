using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BotCore.Core.Test.DomainModels;
using BotCore.Core.Test.Interfaces;
using Newtonsoft.Json;

namespace BotCore.Core.Test.Services
{
    public class BankService : IBankService
    {
        private const string RatesUrl = "https://www.nbrb.by/api/exrates/rates/{0}?parammode=2";
        private const string GetAllCurrenciesUrl = "https://www.nbrb.by/api/exrates/currencies";

        public async Task<Currency> GetCurrency(string currencyName)
        {
            using var client = GetClient();

            var response = await client.GetAsync(string.Format(RatesUrl, currencyName));

            if (!response.IsSuccessStatusCode)
                return null;

            var currency = JsonConvert.DeserializeObject<Currency>(await response.Content.ReadAsStringAsync());
            return currency;
        }

        public async Task<List<Currency>> GetAllCurrencies()
        {
            using var client = GetClient();

            var response = await client.GetAsync(GetAllCurrenciesUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var currency = JsonConvert.DeserializeObject<List<Currency>>(await response.Content.ReadAsStringAsync());
            return currency;
        }

        private HttpClient GetClient()
        {
            return new HttpClient {Timeout = TimeSpan.FromSeconds(30)};
        }
    }
}