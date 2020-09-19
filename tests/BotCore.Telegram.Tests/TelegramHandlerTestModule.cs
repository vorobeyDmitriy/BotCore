using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Handlers;
using BotCore.Tests;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace BotCore.Telegram.Tests
{
    public class TelegramHandlerTestModule : TestModuleBase
    {
        protected override void RegisterDataComponents(IServiceCollection services)
        {
            RegisterActionExecutor(services);
        }

        protected override void LoadHostableComponent(IServiceCollection services)
        {
            services.AddSingleton<IHandler<Update>, TelegramHandler>();
        }

        private void RegisterActionExecutor(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var commands = serviceProvider.GetServices<IAction<TelegramCommand>>();
            var commandExecutor = new ActionExecutor<TelegramCommand>(commands);
            services.AddSingleton<IActionExecutor<TelegramCommand>>(commandExecutor);
        }
    }
}