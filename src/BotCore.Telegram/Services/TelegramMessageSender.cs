using System.Threading;
using System.Threading.Tasks;
using BotCore.Telegram.DomainModels;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using BotCore.Core.DataTransfer;
using BotCore.Core.Interfaces;

namespace BotCore.Telegram.Services
{
    public class TelegramMessageSender : IMessageSender
    {
        private readonly ITelegramBotClient _telegram;

        public TelegramMessageSender(ITelegramBotClient telegram)
        {
            _telegram = telegram;
        }

        public async Task SendTextAsync(Message message)
        {
            if(!(message is TelegramMessage telegramMessage))
                return;
            
            await _telegram.SendTextMessageAsync(message.Receiver, message.Text, 
                telegramMessage.ParseMode, telegramMessage.DisableWebPagePreview, 
                telegramMessage.DisableNotification, telegramMessage.ReplyToMessageId,
                telegramMessage.Keyboard, CancellationToken.None);
        }
    }
}