using System;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Keyboards;

namespace BotCore.Telegram.Test.Actions
{
    public class GetAllCurrenciesAction : TelegramAction
    {
        private const int PageSize = 8;
        private readonly IMessageService _messageService;

        public GetAllCurrenciesAction(IMessageSender<TelegramMessage> messageSender, IMessageService messageService)
            : base(messageSender)
        {
            _messageService = messageService;
        }

        public override async Task ExecuteAsync(TelegramCommand command)
        {
            var count = await _messageService.GetCurrenciesCountAsync();
            var pageCount = Math.Ceiling((double) count / PageSize);
            for (var i = 0; i < pageCount; i++)
            {
                var message = await _messageService.GetAllCurrenciesMessageAsync(i, PageSize);
                await MessageSender.SendTextAsync(new TelegramMessage
                {
                    Receiver = command.SenderId.ToString(),
                    Keyboard = GetCurrencyRateKeyboard.Keyboard,
                    Text = message
                });
            }
        }
    }
}