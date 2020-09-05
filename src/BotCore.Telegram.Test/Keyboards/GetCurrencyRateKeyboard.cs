using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.Test.Keyboards
{
    public static class GetCurrencyRateKeyboard
    {
        public static IReplyMarkup Keyboard => new ReplyKeyboardMarkup
        {
            Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    "Get Currency Rate"
                }, 
            }
        };
    }
}