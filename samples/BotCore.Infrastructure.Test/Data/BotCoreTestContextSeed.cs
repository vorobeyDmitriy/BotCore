﻿using System;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.Test.Entities;
using BotCore.Core.Test.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BotCore.Infrastructure.Test.Data
{
    public class BotCoreTestContextSeed
    {
        public static async Task SeedAsync(BotCoreTestContext context, IBankService bankService, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                // TODO: Only run this if using a real database
                await context.Database.MigrateAsync();

                if (!context.Currency.Any())
                {
                    var currencies = await bankService.GetAllCurrencies();
                    await context.Currency.AddRangeAsync(currencies.Select(x => new Currency
                    {
                        Abbreviation = x.Abbreviation,
                        Name = x.Name,
                        Rate = (decimal) x.OfficialRate,
                        Scale = x.Scale
                    }));

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    await SeedAsync(context, bankService, retryForAvailability);
                }

                throw;
            }
        }
    }
}