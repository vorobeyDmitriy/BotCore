using System.Collections.Generic;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Entities;

namespace BotCore.Core.CurrencyBot.Interfaces
{
    public interface IUsersService
    {
        public Task<User> CreateUserAsync(User user);
        public Task<User> GetUserByUsernameAsync(string username);
        Task<List<Currency>> GetUserDefaultCurrencies(string username);
        Task SetUserDefaultCurrencies(string username, string currencyAbbreviationsText);
    }
}