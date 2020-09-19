using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Viber.CurrencyBot.Keyboards;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;

namespace BotCore.Viber.CurrencyBot.Actions
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