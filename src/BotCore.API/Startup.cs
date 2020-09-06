using BotCore.Core.Interfaces;
using BotCore.Telegram;
using BotCore.Telegram.Test.Actions;
using BotCore.Telegram.Test.Interfaces;
using BotCore.Telegram.Test.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BotCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddSingleton<IAction, StartAction>();
            services.AddSingleton<IAction, GetAllCurrenciesAction>();
            services.AddSingleton<IAction, GetCurrencyRateAction>();
            services.AddSingleton<IBankService, BankService>();
            services.AddTelegram(Configuration.GetSection("TelegramBotToken").Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}