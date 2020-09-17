using BotCore.Core.Test.Constants;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.Test.Keyboards
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
                }
            }
        };
    }
}