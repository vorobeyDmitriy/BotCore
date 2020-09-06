using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Handlers;
using BotCore.Telegram.Interfaces;
using BotCore.Telegram.Services;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace BotCore.Telegram
{
    public static class Startup
    {
        /// <summary>
        ///     Add services for telegram functionality
        /// </summary>
        /// <remarks>
        ///    Use it after all <see cref="IAction{T}"/> registration
        /// </remarks>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="telegramBotToken">Token for telegram bot</param>
        public static void AddTelegram(this IServiceCollection services, string telegramBotToken)
        {
            services.AddSingleton<IMessageSender<TelegramMessage>, TelegramMessageSender>();
            
            var telegramBotClient = new TelegramBotClient(telegramBotToken);
            services.AddSingleton<ITelegramBotClient>(telegramBotClient);
            
            services.AddSingleton<ITelegramHandler, TelegramHandler>();
            
            var serviceProvider = services.BuildServiceProvider();
            var commands = serviceProvider.GetServices<IAction<TelegramCommand>>();
            var commandExecutor = new ActionExecutor<TelegramCommand>(commands);
            services.AddSingleton<IActionExecutor<TelegramCommand>>(commandExecutor);
        }
    }
}