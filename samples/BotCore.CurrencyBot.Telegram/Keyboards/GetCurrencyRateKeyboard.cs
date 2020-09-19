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
                    ActionConstants.GetAllCurrenciesAction
                },
                new KeyboardButton[]
                {
                    ActionConstants.GetCurrencyRateAction
                },
                new KeyboardButton[]
                {
                    ActionConstants.GetConcreteCurrencyRateAction
                },
                new KeyboardButton[]
                {
                    ActionConstants.SetupDefaultCurrencies
                }
            }
        };
    }
}