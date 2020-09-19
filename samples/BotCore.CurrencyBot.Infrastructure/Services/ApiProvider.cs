using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Interfaces;
using Newtonsoft.Json;

namespace BotCore.CurrencyBot.Infrastructure.Services
{
    public class ApiProvider : IApiProvider
    {
        public async Task<T> GetAsync<T>(string url) where T : class
        {
            var response = await SendRequestAsync<object>(HttpMethod.Get, url, null);
            var result = await ProcessResponseAsync<T>(response);

            return result;
        }

        public async Task<T> PostAsync<T, T1>(string url, T1 requestObject) where T : class
        {
            var response = await SendRequestAsync(HttpMethod.Post, url, requestObject);
            var result = await ProcessResponseAsync<T>(response);

            return result;
        }

        private static HttpRequestMessage CreateRequestWithBody<T>(HttpMethod method, string url, T requestObject)
        {
            var content = new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8,
                "application/json");

            return new HttpRequestMessage
            {
                Content = content,
                Method = method,
                RequestUri = new Uri(url)
            };
        }

        private static async Task<T> ProcessResponseAsync<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return default;

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        private static async Task<HttpResponseMessage> SendRequestAsync<T>(HttpMethod method, string url, T model)
        {
            using var request = CreateRequestWithBody(method, url, model);
            using var client = new HttpClient();
            var response = await client.SendAsync(request);
            return response;
        }
    }
}