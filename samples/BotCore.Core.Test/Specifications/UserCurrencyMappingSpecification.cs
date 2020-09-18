using BotCore.Core.Test.Entities;

namespace BotCore.Core.Test.Specifications
{
    public class UserCurrencyMappingSpecification : BaseSpecification<UserCurrencyMapping>
    {
        public UserCurrencyMappingSpecification(int userId) : base(x => x.UserId == userId)
        {
        }
    }
}