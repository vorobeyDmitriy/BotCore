using System.Threading.Tasks;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;

namespace BotCore.Viber.Tests
{
    public class TestAction : ViberAction
    {
        public TestAction(IMessageSender<ViberMessage> messageSender) : base(messageSender)
        {
        }

        public override Task<OperationResult> ExecuteAsync(ViberCommand commandBase)
        {
            return Task.FromResult(new OperationResult());
        }
    }
}