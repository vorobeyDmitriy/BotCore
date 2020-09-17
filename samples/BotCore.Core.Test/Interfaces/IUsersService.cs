using System.Threading.Tasks;
using BotCore.Core.Test.Entities;

namespace BotCore.Core.Test.Interfaces
{
    public interface IUsersService
    {
        public Task<User> GetUserByUsernameAsync(string username);
        public Task<User> CreateUserAsync(User user);
    }
}