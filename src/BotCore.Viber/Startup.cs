using System;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Handlers;
using BotCore.Viber.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Viber.Bot;

namespace BotCore.Viber
{
    public static class Startup
    {
        /// <summary>
        ///     Add services for viber functionality
        /// </summary>
        /// <remarks>
        ///     Use it after all <see cref="IAction{T}" /> registration
        /// </remarks>
        /// <param name="services">
        ///     <see cref="IServiceCollection" />
        /// </param>
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
        ///     Use it after all <see cref="IAction{T}" /> registration
        /// </remarks>
        /// <param name="services">
        ///     <see cref="IServiceCollection" />
        /// </param>
        /// <param name="configuration">Instance of <see cref="IConfiguration" /></param>
        public static async void AddViberClient(this IServiceCollection services, IConfiguration configuration)
        {
            var viberBotToken = configuration.GetSection("Tokens").GetSection("Viber").Value;
            var telegramBotClient = new ViberBotClient(viberBotToken);
            services.AddSingleton<IViberBotClient>(telegramBotClient);
            services.AddSingleton<IMessageSender<ViberMessage>, ViberMessageSender>();
            services.AddSingleton<IHandler<CallbackData>, ViberHandler>();

            await SetWebhook(services, configuration);
        }

        private static async Task SetWebhook(IServiceCollection services, IConfiguration configuration)
        {
            var setViberWebhookUrl = configuration.GetSection("Webhooks").GetSection("Viber").Value;
            var serviceProvider = services.BuildServiceProvider();
            var viber = serviceProvider.GetService<IViberBotClient>();
            var retry = 0;

            while (retry < 10)
                try
                {
                    await Task.Delay(1000);
                    await viber.SetWebhookAsync(setViberWebhookUrl);
                }
                catch (Exception e)
                {
                    retry++;
                    Console.WriteLine(e);
                }
        }
    }
}