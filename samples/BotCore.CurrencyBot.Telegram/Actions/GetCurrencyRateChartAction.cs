using System;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Constants;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.CurrencyBot.Keyboards;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;

namespace BotCore.Telegram.CurrencyBot.Actions
{
    public class GetCurrencyRateChartAction: TelegramAction
    {

        private readonly IPlotService _plotService;
        private readonly ICurrencyService _currencyService;
        
        public GetCurrencyRateChartAction(IMessageSender<TelegramMessage> messageSender, IPlotService plotService, ICurrencyService currencyService)
            : base(messageSender)
        {
            _plotService = plotService;
            _currencyService = currencyService;
        }

        public override async Task<OperationResult> ExecuteAsync(TelegramCommand command)
        {
            var currencies = await
                _currencyService.GetCurrencyRatesInInterval("USD", DateTime.Now.AddDays(-365), DateTime.Now);
            
            return await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.ChatId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = $"{MessagesConstants.Hello}"
            });
        }
    }
}