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
            var telegramMessage = message as TelegramMessage;
            await _telegram.SendTextMessageAsync(message.Receiver, message.Text, 
                ParseMode.Default, false, false,0,
                telegramMessage?.Keyboard, CancellationToken.None);
        }
    }
}