namespace BotCore.Core.CurrencyBot.Constants
{
    public class MessagesConstants
    {
        public const string ConcreteCurrency =
            "Great! Now just write abbreviation of currency (for example USD (or usd)). \r\n" +
            "You can find all abbreviations by choosing Get All Currencies";

        public const string CurrencyNotFound = "Currency not found";

        public const string SetupDefaultCurrencies = "Please, write abbreviations of currencies through the spaces " +
                                                     "(for example usd eur rub).\r\n" +
                                                     "You can find all abbreviations by choosing Get All Currencies";

        public const string SetupDefaultCurrenciesSuccess = "Default currencies was successfully setup";
        
        public const string ConvertCurrency =
            "Great! Now just write abbreviation of currency and amount (for example (usd)USD 1000 (or 1000 usd(USD)). \r\n" +
            "You can find all abbreviations by choosing Get All Currencies";

        public const string Hello = "Hi";
        public const string ArrowUp = "⬆️";
        public const string ArrowDown = "⬇️";
        public const string ArrowRight = "➡️";
    }
}