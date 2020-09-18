using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Core.Test.Entities;

namespace BotCore.Core.Test.Interfaces
{
    public interface IUsersService
    {
        public Task<User> CreateUserAsync(User user);
        public Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<Currency>> GetUserDefaultCurrencies(string username);
        Task SetUserDefaultCurrencies(int userId, string currencyAbbreviationsText);
    }
}