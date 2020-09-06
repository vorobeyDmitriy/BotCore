using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Handlers;
using BotCore.Viber.Interfaces;
using BotCore.Viber.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Viber.Bot;

namespace BotCore.Viber
{
    public static class Startup
    {
        private const string ViberUrl = @"https://chatapi.viber.com/pa/set_webhook";
        /// <summary>
        ///     Add services for viber functionality
        /// </summary>
        /// <remarks>
        ///    Use it after all <see cref="IAction{T}"/> registration
        /// </remarks>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        public static void AddViberActionsExecutor(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var commands = serviceProvider.GetServices<IAction<ViberCommand>>();
            var commandExecutor = new ActionExecutor<ViberCommand>(commands);
            services.AddSingleton<IActionExecutor<ViberCommand>>(commandExecutor);
        }

        /// <summary>
        ///     Add services for viber functionality
        /// </summary>
        /// <remarks>
        ///    Use it after all <see cref="IAction{T}"/> registration
        /// </remarks>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="configuration">Instance of <see cref="IConfiguration"/></param>
        public static async void AddViberClient(this IServiceCollection services, IConfiguration configuration)
        {
            var viberBotToken = configuration.GetSection("Tokens").GetSection("Viber").Value;
            var telegramBotClient = new ViberBotClient(viberBotToken);
            services.AddSingleton<IViberBotClient>(telegramBotClient);
            services.AddSingleton<IMessageSender<ViberMessage>, ViberMessageSender>();
            services.AddSingleton<IViberHandler, ViberHandler>();

            await SetWebhook(configuration);
        }

        private static async Task SetWebhook(IConfiguration configuration)
        {
            using var client = new HttpClient();
            var setTelegramWebhookUrl = configuration.GetSection("Webhooks").GetSection("Telegram").Value;
            await client.GetAsync(setTelegramWebhookUrl);
                
            var setViberWebhookUrl = configuration.GetSection("Webhooks").GetSection("Viber").Value;
            var stringContent = new StringContent(JsonConvert.SerializeObject(new WebhookModel()
                {
                    Url = ViberUrl,
                    SendName = true,
                    SendPhoto = false
                }),
                Encoding.UTF8, "application/json");
            var response = await client.PostAsync(setViberWebhookUrl, stringContent);
            
            if(!response.IsSuccessStatusCode)
                throw new Exception();
        } 
    }
}