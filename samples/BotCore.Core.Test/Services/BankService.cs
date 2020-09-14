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
        private const string RatesWithDateUrl = "https://www.nbrb.by/api/exrates/rates/{0}?parammode=2&ondate={1}";
        private const string GetAllCurrenciesUrl = "https://www.nbrb.by/api/exrates/currencies";

        public async Task<Currency> GetCurrency(string currencyName, DateTime? dateTime = null)
        {
            dateTime ??= DateTime.UtcNow;
            using var client = GetClient();
            var url = string.Format(RatesWithDateUrl, currencyName, dateTime.Value.ToString("yyyy-MM-dd"));
            var response = await client.GetAsync(url);
            var currency = await ProcessResponse<Currency>(response);
            
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