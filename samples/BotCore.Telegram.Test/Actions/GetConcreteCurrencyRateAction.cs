using System;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Constants;
using BotCore.Core.Test.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Keyboards;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.Test.Actions
{
    public class GetConcreteCurrencyRateAction : TelegramAction
    {
        private readonly IMessageService _messageService;

        public GetConcreteCurrencyRateAction(IMessageSender<TelegramMessage> messageSender,
            IMessageService messageService) : base(messageSender)
        {
            _messageService = messageService;
        }

        public override async Task ExecuteAsync(TelegramCommand commandBase)
        {
            if (commandBase.Text?.Length == 3)
                await SendReply(commandBase);
            else
            {
                await MessageSender.SendTextAsync(
                    new TelegramMessage
                    {
                        Keyboard = new ForceReplyMarkup(),
                        Text = $"{ActionConstants.GetConcreteCurrencyRateAction} \r\n " +
                               $"{MessagesConstants.YouChooseConcreteCurrency}",
                        Receiver = commandBase.ChatId.ToString(),
                        ReplyToMessageId = commandBase.MessageId
                    });
            }
        }

        private async Task SendReply(TelegramCommand commandBase)
        {
            var currency = await _messageService.GetCurrencyRateMessageAsync(commandBase.Text, DateTime.UtcNow);

            if (string.IsNullOrWhiteSpace(currency))
                await MessageSender.SendTextAsync(
                    new TelegramMessage
                    {
                        Keyboard = new ForceReplyMarkup(),
                        Text = MessagesConstants.CurrencyNotFound,
                        Receiver = commandBase.ChatId.ToString(),
                        ReplyToMessageId = commandBase.MessageId
                    });

            await MessageSender.SendTextAsync(
                new TelegramMessage
                {
                    Keyboard = GetCurrencyRateKeyboard.Keyboard,
                    Text = currency,
                    Receiver = commandBase.ChatId.ToString(),
                });
        }
    }
}