using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotCore.Common.Test.Interfaces;
using BotCore.Core.Interfaces;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Test.Keyboards;

namespace BotCore.Viber.Test.Actions
{
    public class GetAllCurrenciesAction : ViberAction
    {
        private const int PageSize = 8;
        private readonly IBankService _bankService;

        public GetAllCurrenciesAction(IMessageSender<ViberMessage> messageSender, IBankService bankService) : base(
            messageSender)
        {
            _bankService = bankService;
        }

        public override async Task ExecuteAsync(ViberCommand command)
        {
            var currencies = await _bankService.GetAllCurrencies();
            var sb = new StringBuilder();
            var pageCount = Math.Ceiling((double) currencies.Count / PageSize);

            for (var i = 0; i < pageCount; i++)
            {
                foreach (var currency in currencies.Skip(i * PageSize).Take(PageSize))
                {
                    sb.Append(currency.Name);
                    sb.Append(" ( ");
                    sb.Append(currency.Abbreviation);
                    sb.Append(") ");
                    sb.Append("\r\n");
                }

                await MessageSender.SendTextAsync(new ViberMessage
                {
                    Receiver = command.Receiver,
                    Keyboard = GetCurrencyRateKeyboard.Keyboard,
                    Text = sb.ToString(),
                    SenderDisplayName = "Qwe"
                });
                sb.Clear();
            }
        }
    }
}