using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;

namespace BotCore.Telegram.Test.Actions
{
    public class SetupDefaultCurrenciesAction : TelegramAction
    {
        public SetupDefaultCurrenciesAction(IMessageSender<TelegramMessage> messageSender) : base(messageSender)
        {
        }

        public override Task ExecuteAsync(TelegramCommand commandBase)
        {
            throw new System.NotImplementedException();
        }
    }
}