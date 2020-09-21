using System.Threading.Tasks;
using BotCore.Core;
using BotCore.Core.Interfaces;
using BotCore.Tests;
using BotCore.Tests.DependencyInjection;
using BotCore.Viber.Tests.Modules;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using Viber.Bot;

namespace BotCore.Viber.Tests.Tests
{
    public class ViberHandlerTests : TestClassBase
    {
        protected override IModule Module => new ViberHandlerTestModule();

        [Test]
        public async Task HandleUpdateAsync_EmptyUpdate_UnsuccessfullyResult()
        {
            var handler = GetService<IHandler<CallbackData>>();

            var update = (CallbackData) null;

            var result = await handler.HandleUpdateAsync(update);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.IncomingMessageIsNull, result.Error);
        }

        [Test]
        public async Task HandleUpdateAsync_EmptyMessage_UnsuccessfullyResult()
        {
            var handler = GetService<IHandler<CallbackData>>();

            var update = new CallbackData();

            var result = await handler.HandleUpdateAsync(update);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.IncomingMessageIsNull, result.Error);
        }

        [Test]
        public async Task HandleUpdateAsync_EmptySender_UnsuccessfullyResult()
        {
            var handler = GetService<IHandler<CallbackData>>();

            var update = new CallbackData
            {
                Sender = null
            };

            var result = await handler.HandleUpdateAsync(update);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.IncomingMessageIsNull, result.Error);
        }

        [TestCase(EventType.Action)]
        [TestCase(EventType.ConversationStarted)]
        [TestCase(EventType.Delivered)]
        [TestCase(EventType.Failed)]
        [TestCase(EventType.Seen)]
        [TestCase(EventType.Subscribed)]
        [TestCase(EventType.Unsubscribed)]
        [TestCase(EventType.Webhook)]
        public async Task HandleUpdateAsync_IncorrectEventType_UnsuccessfullyResult(EventType eventType)
        {
            var handler = GetService<IHandler<CallbackData>>();

            var update = new CallbackData
            {
                Sender = new User(),
                Event = eventType,
                Message = new TextMessage()
            };

            var result = await handler.HandleUpdateAsync(update);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.IncomingMessageIsNull, result.Error);
        }
        
        [Test]
        public async Task HandleUpdateAsync_IncorrectMessageType_UnsuccessfullyResult()
        {
            var handler = GetService<IHandler<CallbackData>>();

            var update = new CallbackData
            {
                Sender = new User(),
                Event = EventType.Message,
                Message = new BroadcastMessage()
            };

            var result = await handler.HandleUpdateAsync(update);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.IncomingMessageIsNull, result.Error);
        }
        
        [Test]
        public async Task HandleUpdateAsync_SimpleUpdate_SuccessfullyResult()
        {
            var handler = GetService<IHandler<CallbackData>>();

            var update = DataGenerator.Viber.GetDefaultViberCallbackData(TestConstants.Test);

            var result = await handler.HandleUpdateAsync(update);
            Assert.False(result.Success);
            Assert.AreEqual(TestConstants.Test, result.Error);
        }
    }
}