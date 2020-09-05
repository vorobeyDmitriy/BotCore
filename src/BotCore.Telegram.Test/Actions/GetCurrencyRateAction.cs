using System.Net.Http;
using System.Threading.Tasks;
using BotCore.Core.DataTransfer;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Keyboards;
using Newtonsoft.Json;

namespace BotCore.Telegram.Test.Actions
{
    public class GetCurrencyRateAction : ActionBase
    {
        private readonly IMessageSender _messageSender;

        public GetCurrencyRateAction(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public override async Task ExecuteAsync(MessengerCommandBase commandBase)
        {
            if (!(commandBase is TelegramCommand command))
                return;

            using var client = new HttpClient();

            var usdResponse = await client.GetAsync("https://www.nbrb.by/api/exrates/rates/840?parammode=1");
            var textTemplate = "{0} {1}: {2} BYN";
            var usd = JsonConvert.DeserializeObject<Currency>(await usdResponse.Content.ReadAsStringAsync());
            var usdText = string.Format(textTemplate, usd.Cur_Scale, usd.Cur_Abbreviation, usd.Cur_OfficialRate) +
                          "\r\n";

            var eurResponse = await client.GetAsync("https://www.nbrb.by/api/exrates/rates/978?parammode=1");
            var eur = JsonConvert.DeserializeObject<Currency>(await eurResponse.Content.ReadAsStringAsync());
            var eurText = string.Format(textTemplate, eur.Cur_Scale, eur.Cur_Abbreviation, eur.Cur_OfficialRate) +
                          "\r\n";

            await _messageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.SenderId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = usdText + eurText
            });
        }
    }
}