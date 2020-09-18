using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.Test.Entities;
using BotCore.Core.Test.Interfaces;
using BotCore.Core.Test.Specifications;

namespace BotCore.Core.Test.Services
{
    public class UsersService : IUsersService
    {
        private readonly IAsyncRepository<Currency> _currencyRepository;
        private readonly IAsyncRepository<UserCurrencyMapping> _userCurrencyMappingRepository;
        private readonly IAsyncRepository<User> _userRepository;

        public UsersService(IAsyncRepository<User> userRepository, IAsyncRepository<Currency> currencyRepository,
            IAsyncRepository<UserCurrencyMapping> userCurrencyMappingRepository)
        {
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _userCurrencyMappingRepository = userCurrencyMappingRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var userSpec = new UserSpecification(username);
            var user = (await _userRepository.ListAsync(userSpec)).FirstOrDefault();

            return user;
        }

        public async Task<List<Currency>> GetUserDefaultCurrencies(string username)
        {
            var userSpec = new UserSpecification(username, true);
            var user = (await _userRepository.ListAsync(userSpec)).FirstOrDefault();

            return user?.UserCurrencyMapping.Select(x => x.Currency).ToList();
        }

        public async Task SetUserDefaultCurrencies(string username, string currencyAbbreviationsText)
        {
            var currencyAbbreviations = currencyAbbreviationsText
                .Split(" ")
                .Select(x => x.ToUpperInvariant());

            var currenciesSpec = new CurrencySpecification(currencyAbbreviations);
            var currencies = await _currencyRepository.ListAsync(currenciesSpec);

            var user = await GetUserByUsernameAsync(username);

            var userCurrencyMappingSpecification = new UserCurrencyMappingSpecification(user.Id);
            await _userCurrencyMappingRepository.DeleteAsync(
                await _userCurrencyMappingRepository.ListAsync(userCurrencyMappingSpecification));


            await _userCurrencyMappingRepository.AddAsync(
                currencies.Select(x => new UserCurrencyMapping
                {
                    CurrencyId = x.Id,
                    UserId = user.Id
                }));
        }
    }
}