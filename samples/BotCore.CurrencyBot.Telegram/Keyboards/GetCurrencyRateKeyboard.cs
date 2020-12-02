using BotCore.Core.CurrencyBot.Constants;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.CurrencyBot.Keyboards
{
    public static class GetCurrencyRateKeyboard
    {
        public static IReplyMarkup Keyboard => new ReplyKeyboardMarkup
        {
            Keyboard = new[]
            {
                new KeyboardButton[]
                {
                    ActionConstants.ConvertCurrency,
                    ActionConstants.GetCurrencyRate,
                    ActionConstants.GetConcreteCurrencyRate
                },
                new KeyboardButton[]
                {
                    ActionConstants.GetCurrencyRateChart,
                    ActionConstants.SetupDefaultCurrencies,
                },
                new KeyboardButton[]
                {
                    ActionConstants.GetAllCurrencies
                },
            }
        };
    }
}