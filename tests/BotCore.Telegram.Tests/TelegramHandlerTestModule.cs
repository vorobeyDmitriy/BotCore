using System;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Core.Services;
using BotCore.Telegram.CurrencyBot.Actions;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Handlers;
using BotCore.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
            var commandExecutor = new Mock<ActionExecutor<TelegramCommand>>(commands);

            commandExecutor.Setup(x => x.ExecuteActionAsync(
                        It.Is<TelegramCommand>(c=> c.Text == TestConstants.Test)))
                .Returns(Task.FromResult(new OperationResult(TestConstants.Test)));
            
            commandExecutor.Setup(x => x.ExecuteActionAsync(
                    It.Is<TelegramCommand>(c=> c.Text == TestConstants.ReplyTest)))
                .Returns(Task.FromResult(new OperationResult(TestConstants.Test)));
            
            commandExecutor.Setup(x => x.ExecuteActionAsync(
                    It.Is<TelegramCommand>(c=> c.Text == TestConstants.StartTest)))
                .Returns(Task.FromResult(new OperationResult(TestConstants.StartTest)));
            
            /*commandExecutor.Setup(x => x.ExecuteActionAsync(
                    It.Is<TelegramCommand>(c=> c.Text == TestConstants.ReplyTest)))
                .Returns(Task.FromResult(new OperationResult(TestConstants.ReplyTest)));*/
            
            services.AddSingleton<IActionExecutor<TelegramCommand>>(commandExecutor.Object);
        }
    }
}