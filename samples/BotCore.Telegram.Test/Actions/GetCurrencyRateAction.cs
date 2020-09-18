using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Keyboards;

namespace BotCore.Telegram.Test.Actions
{
    public class GetCurrencyRateAction : TelegramAction
    {
        private readonly IMessageService _messageService;
        private readonly IUsersService _usersService;

        public GetCurrencyRateAction(IMessageSender<TelegramMessage> messageSender, IMessageService messageService, 
            IUsersService usersService)
            : base(messageSender)
        {
            _messageService = messageService;
            _usersService = usersService;
        }

        public override async Task ExecuteAsync(TelegramCommand command)
        {
            var defaultCurrencies = await _usersService.GetUserDefaultCurrencies(command.SenderUsername);

            var sb = new StringBuilder();
            if (defaultCurrencies == null || !defaultCurrencies.Any())
            {
                sb.Append(await _messageService.GetCurrencyRateMessageAsync("USD", DateTime.UtcNow));
                sb.Append(await _messageService.GetCurrencyRateMessageAsync("EUR", DateTime.UtcNow));
                sb.Append(await _messageService.GetCurrencyRateMessageAsync("RUB", DateTime.UtcNow));
            }
            else
            {
                foreach (var currency in defaultCurrencies)
                {
                    sb.Append(await _messageService.GetCurrencyRateMessageAsync(currency.Abbreviation, DateTime.UtcNow));
                }
            }

            await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.ChatId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = sb.ToString()
            });
        }
    }
}