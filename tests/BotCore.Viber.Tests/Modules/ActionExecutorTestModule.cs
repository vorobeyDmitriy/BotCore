using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Tests;
using BotCore.Viber.DomainModels;
using Microsoft.Extensions.DependencyInjection;

namespace BotCore.Viber.Tests.Modules
{
    public class ActionExecutorTestModule : TestModuleBase
    {
        protected override void RegisterDataComponents(IServiceCollection services)
        {
            RegisterEmptyMock<IMessageSender<ViberMessage>>(services);
            services.AddSingleton<IAction<ViberCommand>, TestAction>();
        }

        protected override void LoadHostableComponent(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var actions = sp.GetServices<IAction<ViberCommand>>();
            var actionExecutor = new ActionExecutor<ViberCommand>(actions);
            services.AddSingleton<IActionExecutor<ViberCommand>>(actionExecutor);
        }
    }
}