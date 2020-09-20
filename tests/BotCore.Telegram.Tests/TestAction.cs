using System.Threading.Tasks;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;

namespace BotCore.Telegram.Tests
{
    public class TestAction : TelegramAction
    {
        public TestAction(IMessageSender<TelegramMessage> messageSender) : base(messageSender)
        {
        }

        public override Task<OperationResult> ExecuteAsync(TelegramCommand commandBase)
        {
            return Task.FromResult(new OperationResult());
        }
    }
}