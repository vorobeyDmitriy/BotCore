using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Handlers;
using BotCore.Viber.Interfaces;
using BotCore.Viber.Services;
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
        /// <param name="viberBotToken">Token for viber bot</param>
        public static void AddViberClient(this IServiceCollection services, string viberBotToken)
        {
            var telegramBotClient = new ViberBotClient(viberBotToken);
            services.AddSingleton<IViberBotClient>(telegramBotClient);
            services.AddSingleton<IMessageSender<ViberMessage>, ViberMessageSender>();
            services.AddSingleton<IViberHandler, ViberHandler>();
        }
    }
}