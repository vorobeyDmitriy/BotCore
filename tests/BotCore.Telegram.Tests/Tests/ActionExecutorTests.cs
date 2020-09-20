using System.Threading.Tasks;
using BotCore.Core;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Tests.Modules;
using BotCore.Tests;
using BotCore.Tests.DependencyInjection;
using NUnit.Framework;

namespace BotCore.Telegram.Tests.Tests
{
    public class ActionExecutorTests : TestClassBase
    {
        protected override IModule Module => new ActionExecutorTestModule();

        [Test]
        public async Task ExecuteActionAsync_EmptyCommand_CommandNotFount()
        {
            var actionExecutor = GetService<IActionExecutor<TelegramCommand>>();

            var command = (TelegramCommand) null;

            var result = await actionExecutor.ExecuteActionAsync(command);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.CommandNotFound, result.Error);
        }

        [Test]
        public async Task ExecuteActionAsync_NotExistedCommand_CommandNotFount()
        {
            var actionExecutor = GetService<IActionExecutor<TelegramCommand>>();

            var command = new TelegramCommand("Not existed");

            var result = await actionExecutor.ExecuteActionAsync(command);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.CommandNotFound, result.Error);
        }

        [Test]
        public async Task ExecuteActionAsync_CorrectCommand_SuccessfullyResult()
        {
            var actionExecutor = GetService<IActionExecutor<TelegramCommand>>();

            var command = new TelegramCommand(nameof(TestAction).Replace(TestConstants.Action, string.Empty));

            var result = await actionExecutor.ExecuteActionAsync(command);
            Assert.True(result.Success);
            Assert.AreEqual("", result.Error);
        }
    }
}