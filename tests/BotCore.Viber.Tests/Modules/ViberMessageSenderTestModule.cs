using System.Threading;
using System.Threading.Tasks;
using BotCore.Core;
using BotCore.Core.Interfaces;
using BotCore.Tests;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Viber.Bot;

namespace BotCore.Viber.Tests.Modules
{
    public class ViberMessageSenderTestModule : TestModuleBase
    {
        protected override void RegisterDataComponents(IServiceCollection services)
        {
            RegisterTelegramBotClient(services);
        }

        protected override void LoadHostableComponent(IServiceCollection services)
        {
            services.AddSingleton<IMessageSender<ViberMessage>, ViberMessageSender>();
        }

        private void RegisterTelegramBotClient(IServiceCollection services)
        {
            var mock = new Mock<IViberBotClient>();

            mock.Setup(x => x.SendKeyboardMessageAsync(It.Is<KeyboardMessage>(
                    c=>c.Text == TestConstants.Test)))
                .Returns(Task.FromResult((long) 1));

            mock.Setup(x => x.SendKeyboardMessageAsync(It.Is<KeyboardMessage>(
                    c=>c.Text == TestConstants.Action)))
                .Returns(Task.FromResult((long) 0));


            services.AddSingleton(mock.Object);
        }
    }
}