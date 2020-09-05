using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Interfaces;
using BotCore.Telegram.Test.Keyboards;

namespace BotCore.Telegram.Test.Actions
{
    public class GetAllCurrenciesAction : TelegramActionBase
    {
        private readonly IBankService _bankService;
        private const int PageSize = 8;
        
        public GetAllCurrenciesAction(IMessageSender messageSender, IBankService bankService) : base(messageSender)
        {
            _bankService = bankService;
        }
        
        public override async Task ExecuteAsync(MessengerCommandBase commandBase)
        {
            if (!(commandBase is TelegramCommand command))
                return;
            
            var currencies = await _bankService.GetAllCurrencies();
            var sb = new StringBuilder();
            var pageCount = Math.Ceiling((double)currencies.Count / PageSize);
            
            for (var i = 0; i < pageCount; i++)
            {
                foreach (var currency in currencies.Skip(i*PageSize).Take(PageSize))
                {
                    sb.Append(currency.Name);
                    sb.Append(" ( ");
                    sb.Append(currency.Abbreviation);
                    sb.Append(") ");
                    sb.Append("\r\n");
                }

                await MessageSender.SendTextAsync(new TelegramMessage
                {
                    Receiver = command.SenderId.ToString(),
                    Keyboard = GetCurrencyRateKeyboard.Keyboard,
                    Text = sb.ToString()
                });
                sb.Clear();
            }
        }
    }
}