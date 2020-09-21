using System;
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
    public class ViberMessageSenderTests : TestClassBase
    {
        protected override IModule Module => new ViberMessageSenderTestModule();

        [Test]
        public void SendTextAsync_NullMessage_ThrowsArgumentNullException()
        {
            var messageSender = GetService<IMessageSender<ViberMessage>>();

            var message = (ViberMessage) null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => await messageSender.SendTextAsync(message));
        }

        [Test]
        public async Task SendTextAsync_UnsuccessfullySend_UnsuccessfullyResult()
        {
            var messageSender = GetService<IMessageSender<ViberMessage>>();

            var message = new ViberMessage
            {
                Receiver = "-1",
                Text = TestConstants.Action,
                Keyboard = null,
            };

            var result = await messageSender.SendTextAsync(message);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.Error, result.Error);
        }

        [Test]
        public async Task SendTextAsync_SuccessfullySend_SuccessfullyResult()
        {
            var messageSender = GetService<IMessageSender<ViberMessage>>();

            var message = new ViberMessage
            {
                Receiver = "-1",
                Text = TestConstants.Test,
                Keyboard = null,
                SenderDisplayName = "1"
            };

            var result = await messageSender.SendTextAsync(message);
            Assert.True(result.Success);
            Assert.AreEqual(string.Empty, result.Error);
        }
    }
}