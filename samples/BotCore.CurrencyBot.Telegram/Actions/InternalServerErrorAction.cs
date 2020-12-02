using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Constants;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.CurrencyBot.Keyboards;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;

namespace BotCore.Telegram.CurrencyBot.Actions
{
    public class InternalServerErrorAction: TelegramAction
    {
        public InternalServerErrorAction(IMessageSender<TelegramMessage> messageSender) : base(messageSender)
        {
        }

        public override async Task<OperationResult> ExecuteAsync(TelegramCommand commandBase)
        {
            return await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = commandBase.ChatId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = MessagesConstants.InternalServerError
            });
        }
    }
}