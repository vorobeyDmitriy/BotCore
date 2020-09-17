using System;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.Test.Entities;
using BotCore.Core.Test.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BotCore.Infrastructure.Test.Data
{
    public class BotCoreTestContextSeed
    {
        public static async Task SeedAsync(BotCoreTestContext context, ICurrencyService currencyService, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                // TODO: Only run this if using a real database
                await context.Database.MigrateAsync();

                if (!context.Currency.Any())
                {
                    var currencies = await currencyService.GetAllCurrencies();
                    await context.Currency.AddRangeAsync(currencies.Select(x => new Currency
                    {
                        Abbreviation = x.Abbreviation,
                        Name = x.Name,
                        Rate = (decimal) x.OfficialRate,
                        Scale = x.Scale
                    }));
                    await context.Currency.AddAsync(GetByn());

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                if (retryForAvailability >= 10)
                    throw;

                retryForAvailability++;
                await SeedAsync(context, currencyService, retryForAvailability);

                throw;
            }
        }

        private static Currency GetByn()
        {
            return new Currency
            {
                Abbreviation = "BYN",
                Name = "Белорусский рубль",
                Scale = 1,
                Rate = 1
            };
        }
    }
}