using System.Collections.Generic;
using Viber.Bot;

namespace BotCore.Viber.Test.Keyboards
{
    public static class GetCurrencyRateKeyboard
    {
        public static Keyboard Keyboard => new Keyboard
        {
            Buttons = new List<KeyboardButton>
            {
                new KeyboardButton
                {
                    Text = "Get All Currencies"
                },
                new KeyboardButton
                {
                    Text = "Get Currency Rate"
                }
            }
        };
    }
}