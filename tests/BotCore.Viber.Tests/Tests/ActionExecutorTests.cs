using System.Threading.Tasks;
using BotCore.Core;
using BotCore.Core.Interfaces;
using BotCore.Tests;
using BotCore.Tests.DependencyInjection;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Tests.Modules;
using NUnit.Framework;

namespace BotCore.Viber.Tests.Tests
{
    public class ActionExecutorTests : TestClassBase
    {
        protected override IModule Module => new ActionExecutorTestModule();

        [Test]
        public async Task ExecuteActionAsync_EmptyCommand_CommandNotFount()
        {
            var actionExecutor = GetService<IActionExecutor<ViberCommand>>();

            var command = (ViberCommand) null;

            var result = await actionExecutor.ExecuteActionAsync(command);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.CommandNotFound, result.Error);
        }

        [Test]
        public async Task ExecuteActionAsync_NotExistedCommand_CommandNotFount()
        {
            var actionExecutor = GetService<IActionExecutor<ViberCommand>>();

            var command = new ViberCommand("Not existed");

            var result = await actionExecutor.ExecuteActionAsync(command);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.CommandNotFound, result.Error);
        }

        [Test]
        public async Task ExecuteActionAsync_CorrectCommand_SuccessfullyResult()
        {
            var actionExecutor = GetService<IActionExecutor<ViberCommand>>();

            var command = new ViberCommand(nameof(TestAction)
                .Replace(TestConstants.Action, string.Empty))
            {
                Receiver = "1",
            };

            var result = await actionExecutor.ExecuteActionAsync(command);
            Assert.True(result.Success);
            Assert.AreEqual(string.Empty, result.Error);
        }
    }
}