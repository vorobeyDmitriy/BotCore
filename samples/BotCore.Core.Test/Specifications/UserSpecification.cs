using BotCore.Core.Test.Entities;

namespace BotCore.Core.Test.Specifications
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