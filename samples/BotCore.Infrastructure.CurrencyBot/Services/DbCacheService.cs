using System;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.DomainModels;
using BotCore.Core.CurrencyBot.Entities;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.CurrencyBot.Specifications;
using CurrencyEntity = BotCore.Core.CurrencyBot.Entities.Currency;

namespace BotCore.Infrastructure.CurrencyBot.Services
{
    public class DbCacheService : IDbCacheService
    {
        private const string Byn = "BYN";
        private readonly IAsyncRepository<CurrencyRate> _currencyRateRepository;
        private readonly IAsyncRepository<CurrencyEntity> _currencyRepository;

        public DbCacheService(IAsyncRepository<CurrencyRate> currencyRateRepository,
            IAsyncRepository<CurrencyEntity> currencyRepository)
        {
            _currencyRateRepository = currencyRateRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<CurrencyRate> GetCurrencyRateFromCacheAsync(string currencyAbbreviation, DateTime dateTime)
        {
            var currencyRateSpec = new CurrencyRateSpecification(currencyAbbreviation, dateTime);
            var currencyFromDb = (await _currencyRateRepository.ListAsync(currencyRateSpec))
                .FirstOrDefault();

            return currencyFromDb;
        }

        public async Task SetCurrencyRateToCacheAsync(CurrencyModel currencyModel, DateTime dateTime)
        {
            var currencySpec = new CurrencySpecification(currencyModel.Abbreviation);
            var from = (await _currencyRepository.ListAsync(currencySpec)).FirstOrDefault();
            currencySpec = new CurrencySpecification(Byn);
            var to = (await _currencyRepository.ListAsync(currencySpec)).FirstOrDefault();

            if (from == null || to == null)
                return;

            var entity = new CurrencyRate
            {
                Rate = currencyModel.OfficialRate,
                Date = dateTime.Date,
                FromId = from.Id,
                ToId = to.Id
            };
            await _currencyRateRepository.AddAsync(entity);
        }
    }
}