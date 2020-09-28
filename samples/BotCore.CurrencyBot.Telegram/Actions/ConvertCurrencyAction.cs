using System;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Constants;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.CurrencyBot.Keyboards;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.CurrencyBot.Actions
{
    public class ConvertCurrencyAction: TelegramAction
    {
        private readonly ICurrencyService _currencyService;
        private readonly IMessageService _messageService;

        public ConvertCurrencyAction(IMessageSender<TelegramMessage> messageSender,
            IMessageService messageService, ICurrencyService currencyService) : base(messageSender)
        {
            _messageService = messageService;
            _currencyService = currencyService;
        }

        public override async Task<OperationResult> ExecuteAsync(TelegramCommand commandBase)
        {
            if (!IsFirstStepMessage(commandBase.Text, ActionConstants.ConvertCurrency))
                return await SendReply(commandBase);

            return await MessageSender.SendTextAsync(
                new TelegramMessage
                {
                    Keyboard = new ForceReplyMarkup(),
                    Text = $"{ActionConstants.ConvertCurrency} \r\n " +
                           $"{MessagesConstants.ConvertCurrency}",
                    Receiver = commandBase.ChatId.ToString(),
                    ReplyToMessageId = commandBase.MessageId
                });
        }

        private async Task<OperationResult> SendReply(TelegramCommand commandBase)
        {
            var textParts = commandBase.Text.Split(" ");

            string currencyAbbreviation;
            if (double.TryParse(textParts.FirstOrDefault(), out var amount))
            {
                currencyAbbreviation = textParts.LastOrDefault();
            }
            else
            {
                if (!double.TryParse(textParts.LastOrDefault(), out amount))
                {
                    return await MessageSender.SendTextAsync(
                        new TelegramMessage
                        {
                            Keyboard = new ForceReplyMarkup(),
                            Text = MessagesConstants.CurrencyNotFound,
                            Receiver = commandBase.ChatId.ToString(),
                            ReplyToMessageId = commandBase.MessageId
                        });
                }
                currencyAbbreviation = textParts.FirstOrDefault();
   
            }
            var currency = await _currencyService.GetCurrency(currencyAbbreviation, DateTime.UtcNow);
            var message = _messageService.GetCurrencyRateMessageAsync(currency, amount);

            return await MessageSender.SendTextAsync(
                new TelegramMessage
                {
                    Keyboard = GetCurrencyRateKeyboard.Keyboard,
                    Text = message,
                    Receiver = commandBase.ChatId.ToString()
                });
        }
    }
}