using BotCore.Core.CurrencyBot.Entities;

namespace BotCore.Core.CurrencyBot.Specifications
{
    public class UserCurrencyMappingSpecification : BaseSpecification<UserCurrencyMapping>
    {
        public UserCurrencyMappingSpecification(int userId) : base(x => x.UserId == userId)
        {
        }
    }
}