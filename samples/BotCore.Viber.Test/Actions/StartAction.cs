using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Test.Keyboards;

namespace BotCore.Viber.Test.Actions
{
    public class StartAction : ViberAction
    {
        public StartAction(IMessageSender<ViberMessage> messageSender) : base(messageSender)
        {
        }

        public override async Task ExecuteAsync(ViberCommand commandBase)
        {
            await MessageSender.SendTextAsync(new ViberMessage
            {
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Receiver = commandBase.Receiver,
                Text = "Hi!",
                SenderDisplayName = "Currency Bot"
            });
        }
    }
}