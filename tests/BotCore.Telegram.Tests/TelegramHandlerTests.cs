using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Tests;
using BotCore.Tests.DependencyInjection;
using NUnit.Framework;
using Telegram.Bot.Types;

namespace BotCore.Telegram.Tests
{
    public class TelegramHandlerTests : TestClassBase
    {
        protected override IModule Module => new TelegramHandlerTestModule();

        [Test]
        public async Task HandleUpdate_EmptyUpdate_UnsuccessfullyResult()
        {
            var handler = GetService<IHandler<Update>>();

            var update = (Update) null;

            var result = await handler.HandleUpdate(update);
            Assert.False(result.Success);
        }

        [Test]
        public async Task HandleUpdate_EmptyMessage_UnsuccessfullyResult()
        {
            var handler = GetService<IHandler<Update>>();

            var update = new Update();

            var result = await handler.HandleUpdate(update);
            Assert.False(result.Success);
        }

        [Test]
        public async Task HandleUpdate_EmptyChat_UnsuccessfullyResult()
        {
            var handler = GetService<IHandler<Update>>();

            var update = new Update
            {
                Message = new Message
                {
                    Chat = null
                }
            };

            var result = await handler.HandleUpdate(update);
            Assert.False(result.Success);
        }
    }
}