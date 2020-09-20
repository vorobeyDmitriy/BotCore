using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Telegram.DomainModels;
using BotCore.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace BotCore.Telegram.Tests.Modules
{
    public class ActionExecutorTestModule : TestModuleBase
    {
        protected override void RegisterDataComponents(IServiceCollection services)
        {
            RegisterEmptyMock<IMessageSender<TelegramMessage>>(services);
            services.AddSingleton<IAction<TelegramCommand>, TestAction>();
        }

        protected override void LoadHostableComponent(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var actions = sp.GetServices<IAction<TelegramCommand>>();
            var actionExecutor = new ActionExecutor<TelegramCommand>(actions);
            services.AddSingleton<IActionExecutor<TelegramCommand>>(actionExecutor);
        }
    }
}