using System;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.CurrencyBot.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace BotCore.CurrencyBot.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var catalogContext = services.GetRequiredService<BotCoreTestContext>();
                    var bankService = services.GetRequiredService<ICurrencyService>();
                    await BotCoreTestContextSeed.SeedAsync(catalogContext, bankService);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred seeding the DB.");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseNLog()
                .ConfigureLogging(
                    logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Error);
                        logging.AddNLog("nlog.config");
                    });
        }
    }
}