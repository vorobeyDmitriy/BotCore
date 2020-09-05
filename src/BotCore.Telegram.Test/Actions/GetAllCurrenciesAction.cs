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

            foreach (var currency in currencies)
            {
                sb.Append(currency.Name);
                sb.Append(" ( ");
                sb.Append(currency.Abbreviation);
                sb.Append(") ");
            }

            sb.Remove(sb.Length - 3, 2);
            await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.SenderId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                //TODO: too long string
                Text = "sb.ToString()"
            });
        }
    }
}