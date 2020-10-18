using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BotCore.Core;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

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

        public async Task<OperationResult> SendTextAsync(TelegramMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(TelegramMessage));
           
            var result = await _telegram.SendTextMessageAsync(message.Receiver, message.Text,
                message.ParseMode, message.DisableWebPagePreview,
                message.DisableNotification, message.ReplyToMessageId,
                message.Keyboard, CancellationToken.None);

            return result == null
                ? new OperationResult(Constants.Error)
                : new OperationResult();
        }

        public async Task<OperationResult> SendPictureAsync(TelegramMessage message, string picturePath)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(TelegramMessage));

            await using var stream = new FileInfo(picturePath).OpenRead();
            var result = await _telegram.SendPhotoAsync(message.Receiver, new InputOnlineFile(stream), 
                message.Text, message.ParseMode, message.DisableNotification, message.ReplyToMessageId,
                message.Keyboard, CancellationToken.None);

            return result == null
                ? new OperationResult(Constants.Error)
                : new OperationResult();
        }
    }
}