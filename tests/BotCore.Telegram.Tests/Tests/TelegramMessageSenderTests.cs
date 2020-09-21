using System;
using System.Threading.Tasks;
using BotCore.Core;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Tests.Modules;
using BotCore.Tests;
using BotCore.Tests.DependencyInjection;
using NUnit.Framework;
using Telegram.Bot.Types.Enums;

namespace BotCore.Telegram.Tests.Tests
{
    public class TelegramMessageSenderTests : TestClassBase
    {
        protected override IModule Module => new TelegramMessageSenderTestModule();

        [Test]
        public void SendTextAsync_NullMessage_ThrowsArgumentNullException()
        {
            var messageSender = GetService<IMessageSender<TelegramMessage>>();

            var message = (TelegramMessage) null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => await messageSender.SendTextAsync(message));
        }

        [Test]
        public async Task SendTextAsync_UnsuccessfullySend_UnsuccessfullyResult()
        {
            var messageSender = GetService<IMessageSender<TelegramMessage>>();

            var message = new TelegramMessage
            {
                Receiver = "-1",
                Text = "test",
                Keyboard = null,
                DisableNotification = false,
                ParseMode = ParseMode.Default,
                DisableWebPagePreview = false,
                ReplyToMessageId = 1
            };

            var result = await messageSender.SendTextAsync(message);
            Assert.False(result.Success);
            Assert.AreEqual(Constants.Error, result.Error);
        }

        [Test]
        public async Task SendTextAsync_SuccessfullySend_SuccessfullyResult()
        {
            var messageSender = GetService<IMessageSender<TelegramMessage>>();

            var message = new TelegramMessage
            {
                Receiver = "123",
                Text = "test"
            };

            var result = await messageSender.SendTextAsync(message);
            Assert.True(result.Success);
            Assert.AreEqual(string.Empty, result.Error);
        }
    }
}