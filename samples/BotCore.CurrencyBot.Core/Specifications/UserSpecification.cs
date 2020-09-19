using BotCore.Core.CurrencyBot.Entities;

namespace BotCore.Core.CurrencyBot.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string username, bool includeCurrencyMapping = false) : base(x =>
            x.Username == username)
        {
            if (!includeCurrencyMapping)

                return;
            AddInclude("UserCurrencyMapping.User");
            AddInclude("UserCurrencyMapping.Currency");
        }
    }
}