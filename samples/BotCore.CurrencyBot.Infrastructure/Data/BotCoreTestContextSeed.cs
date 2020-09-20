using System;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Entities;
using BotCore.Core.CurrencyBot.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BotCore.CurrencyBot.Infrastructure.Data
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
                    await context.Currency.AddRangeAsync(currencies);
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