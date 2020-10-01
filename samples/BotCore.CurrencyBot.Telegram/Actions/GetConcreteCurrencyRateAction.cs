using System;
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
    public class GetConcreteCurrencyRateAction : TelegramAction
    {
        private readonly ICurrencyService _currencyService;
        private readonly IMessageService _messageService;

        public GetConcreteCurrencyRateAction(IMessageSender<TelegramMessage> messageSender,
            IMessageService messageService, ICurrencyService currencyService) : base(messageSender)
        {
            _messageService = messageService;
            _currencyService = currencyService;
        }

        public override async Task<OperationResult> ExecuteAsync(TelegramCommand commandBase)
        {
            if (!IsFirstStepMessage(commandBase.Text, ActionConstants.GetConcreteCurrencyRateAction))
                return await SendReply(commandBase);

            return await MessageSender.SendTextAsync(
                new TelegramMessage
                {
                    Keyboard = new ForceReplyMarkup(),
                    Text = $"{ActionConstants.GetConcreteCurrencyRateAction} \r\n " +
                           $"{MessagesConstants.ConcreteCurrency}",
                    Receiver = commandBase.ChatId.ToString(),
                    ReplyToMessageId = commandBase.MessageId
                });
        }

        private async Task<OperationResult> SendReply(TelegramCommand commandBase)
        {
            var gain = await _currencyService.GetCurrencyRateGain(commandBase.Text, DateTime.UtcNow);
            var currency = _messageService.GetCurrencyRateGainMessageAsync(gain);

            if (string.IsNullOrWhiteSpace(currency))
                return await MessageSender.SendTextAsync(
                    new TelegramMessage
                    {
                        Keyboard = new ForceReplyMarkup(),
                        Text = MessagesConstants.CurrencyNotFound,
                        Receiver = commandBase.ChatId.ToString(),
                        ReplyToMessageId = commandBase.MessageId
                    });

            return await MessageSender.SendTextAsync(
                new TelegramMessage
                {
                    Keyboard = GetCurrencyRateKeyboard.Keyboard,
                    Text = $"{gain.Date:M}{MessagesConstants.NextLine}{currency}",
                    Receiver = commandBase.ChatId.ToString()
                });
        }
    }
}