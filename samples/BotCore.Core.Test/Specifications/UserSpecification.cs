using BotCore.Core.Test.Entities;

namespace BotCore.Core.Test.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string username) : base(x=>x.Username == username)
        {
        }
    }
}