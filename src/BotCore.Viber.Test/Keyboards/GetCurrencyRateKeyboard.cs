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
                    Text = "Get All Currencies",
                    ActionType = KeyboardActionType.Reply,
                    ActionBody = "Get Currency Rate"
                },
                new KeyboardButton
                {
                    Text = "Get Currency Rate",
                    ActionType = KeyboardActionType.Reply,
                    ActionBody = "Get Currency Rate"
                }
            },
        };
    }
}