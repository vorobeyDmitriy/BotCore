using System.Threading;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Services;
using BotCore.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.Tests.Modules
{
    public class TelegramMessageSenderTestModule : TestModuleBase
    {
        protected override void RegisterDataComponents(IServiceCollection services)
        {
            RegisterTelegramBotClient(services);
        }

        protected override void LoadHostableComponent(IServiceCollection services)
        {
            services.AddSingleton<IMessageSender<TelegramMessage>, TelegramMessageSender>();
        }

        private void RegisterTelegramBotClient(IServiceCollection services)
        {
            var mock = new Mock<ITelegramBotClient>();

            mock.Setup(x => x.SendTextMessageAsync(It.Is<ChatId>(c => c.Identifier > 0),
                    It.IsAny<string>(), It.IsAny<ParseMode>(), It.IsAny<bool>(),
                    It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Message()));

            mock.Setup(x => x.SendTextMessageAsync(It.Is<ChatId>(c => c.Identifier < 0),
                    It.IsAny<string>(), It.IsAny<ParseMode>(), It.IsAny<bool>(),
                    It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IReplyMarkup>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Message>(null));

            services.AddSingleton(mock.Object);
        }
    }
}