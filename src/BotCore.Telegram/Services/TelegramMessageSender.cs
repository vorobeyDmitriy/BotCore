using System.Threading;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using Telegram.Bot;

namespace BotCore.Telegram.Services
{
    /// <inheritdoc cref="IMessageSender{T}" />
    public class TelegramMessageSender : IMessageSender<TelegramMessage>
    {
        private readonly ITelegramBotClient _telegram;

        public TelegramMessageSender(ITelegramBotClient telegram)
        {
            _telegram = telegram;
        }

        public async Task SendTextAsync(TelegramMessage message)
        {
            await _telegram.SendTextMessageAsync(message.Receiver, message.Text,
                message.ParseMode, message.DisableWebPagePreview,
                message.DisableNotification, message.ReplyToMessageId,
                message.Keyboard, CancellationToken.None);
        }
    }
}