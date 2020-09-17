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
                    "Get All Currencies"
                },
                new KeyboardButton[]
                {
                    "Get Currency Rate"
                },
                new KeyboardButton[]
                {
                    "Get Concrete Currency Rate"
                }
            }
        };
    }
}