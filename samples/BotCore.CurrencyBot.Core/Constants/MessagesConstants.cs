namespace BotCore.Core.CurrencyBot.Constants
{
    public class MessagesConstants
    {
        public const string NextLine = "\r\n";

        public const string ConcreteCurrency =
            "Great! Now just write abbreviation of currency (for example USD (or usd))." + NextLine +
            "You can find all abbreviations by choosing Get All Currencies";

        public const string CurrencyNotFound = "Currency not found";

        public const string SetupDefaultCurrencies = "Please, write abbreviations of currencies through the spaces " +
                                                     "(for example usd eur rub)" + NextLine +
                                                     "You can find all abbreviations by choosing Get All Currencies";

        public const string SetupDefaultCurrenciesSuccess = "Default currencies was successfully setup";

        public const string ConvertCurrency =
            "Great! Now just write abbreviation of currency and amount (for example (usd)USD 1000 (or 1000 usd(USD))." +
            NextLine + "You can find all abbreviations by choosing Get All Currencies";

        public const string SelectDateInterval = "Please select a time interval (in months) for plotting." + NextLine +
                                                 "Charts will be created for each favourite currency.";

        public const string InternalServerError = "Something going wrong :(" + NextLine + "Try again later";

        public const string Hello = "Hi";
        public const string ArrowUp = "⬆️";
        public const string ArrowDown = "⬇️";
        public const string ArrowRight = "➡️";
    }
}