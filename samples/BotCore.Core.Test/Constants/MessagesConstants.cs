namespace BotCore.Core.Test.Constants
{
    public class MessagesConstants
    {
        public const string YouChooseConcreteCurrency =
            "Great! Now just write abbreviation of currency (for example USD (or usd)). \r\n" +
            "You can find all abbreviations by choosing Get All Currencies";
        
        public const string CurrencyNotFound = "Currency not found";
        
        public const string SetupDefaultCurrencies = "Please, write abbreviations of currencies through the spaces " +
                                                     "(for example usd eur rub).\r\n" +
                                                     "You can find all abbreviations by choosing Get All Currencies";
        
        public const string SetupDefaultCurrenciesSuccess = "Default currencies was successfully setup";

        public const string Hello = "Hi";
        public const string ArrowUp = "⬆️";
        public const string ArrowDown = "⬇️";
        public const string ArrowRight = "➡️";
    }
}