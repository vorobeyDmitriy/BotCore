using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.Test.Entities;
using BotCore.Core.Test.Interfaces;
using BotCore.Core.Test.Specifications;

namespace BotCore.Core.Test.Services
{
    public class UsersService : IUsersService
    {
        private readonly IAsyncRepository<User> _userRepository;

        public UsersService(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var userSpec = new UserSpecification(username);
            var user = (await _userRepository.ListAsync(userSpec)).FirstOrDefault();

            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }
    }
}