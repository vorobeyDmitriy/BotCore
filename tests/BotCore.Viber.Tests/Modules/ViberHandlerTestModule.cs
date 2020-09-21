using System.Threading.Tasks;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Tests;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Viber.Bot;

namespace BotCore.Viber.Tests.Modules
{
    public class ViberHandlerTestModule : TestModuleBase
    {
        protected override void RegisterDataComponents(IServiceCollection services)
        {
            RegisterActionExecutor(services);
        }

        protected override void LoadHostableComponent(IServiceCollection services)
        {
            services.AddSingleton<IHandler<CallbackData>, ViberHandler>();
        }

        private void RegisterActionExecutor(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var commands = serviceProvider.GetServices<IAction<ViberCommand>>();
            var commandExecutor = new Mock<ActionExecutor<ViberCommand>>(commands);

            commandExecutor.Setup(x => x.ExecuteActionAsync(
                    It.Is<ViberCommand>(c => c.CommandName == TestConstants.Test)))
                .Returns(Task.FromResult(new OperationResult(TestConstants.Test)));

            services.AddSingleton<IActionExecutor<ViberCommand>>(commandExecutor.Object);
        }
    }
}