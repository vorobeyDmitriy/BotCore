using System;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Handlers;
using BotCore.Telegram.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotCore.Telegram
{
    public static class TelegramStartup
    {
        /// <summary>
        ///     Add services for telegram functionality
        /// </summary>
        /// <remarks>
        ///     Use it after all <see cref="IAction{T}" /> registration
        /// </remarks>
        /// <param name="services">
        ///     <see cref="IServiceCollection" />
        /// </param>
        public static void AddTelegramActionsExecutor(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var commands = serviceProvider.GetServices<IAction<TelegramCommand>>();
            var commandExecutor = new ActionExecutor<TelegramCommand>(commands);
            services.AddSingleton<IActionExecutor<TelegramCommand>>(commandExecutor);
        }

        /// <summary>
        ///     Add services for telegram functionality
        /// </summary>
        /// <remarks>
        ///     Use it after all <see cref="IAction{T}" /> registration
        /// </remarks>
        /// <param name="services">
        ///     <see cref="IServiceCollection" />
        /// </param>
        /// <param name="configuration">Instance of <see cref="IConfiguration" /></param>
        /// <param name="isProduction"></param>
        public static async void AddTelegramClient(this IServiceCollection services, IConfiguration configuration,
            bool isProduction)
        {
            var telegramBotToken = isProduction 
                ? Environment.GetEnvironmentVariable("TelegramToken")
                : configuration.GetSection("Tokens").GetSection("Telegram").Value;
            var telegramBotClient = new TelegramBotClient(telegramBotToken);
            services.AddSingleton<ITelegramBotClient>(telegramBotClient);
            services.AddScoped<IMessageSender<TelegramMessage>, TelegramMessageSender>();
            services.AddScoped<IHandler<Update>, TelegramHandler>();

            await SetWebhook(services, configuration);
        }


        private static async Task SetWebhook(IServiceCollection services, IConfiguration configuration)
        {
            var setTelegramWebhookUrl = configuration.GetSection("Webhooks").GetSection("Telegram").Value;
            var serviceProvider = services.BuildServiceProvider();
            var telegram = serviceProvider.GetService<ITelegramBotClient>();
            await telegram.SetWebhookAsync(setTelegramWebhookUrl);
        }
    }
}