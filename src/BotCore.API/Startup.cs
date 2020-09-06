using System.Collections.Generic;
using BotCore.Core.Interfaces;
using BotCore.Telegram;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Interfaces;
using BotCore.Telegram.Test.Actions;
using BotCore.Telegram.Test.Interfaces;
using BotCore.Telegram.Test.Services;
using BotCore.Viber;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;
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

            services.AddTelegramClient(Configuration.GetSection("Tokens").GetSection("Telegram").Value);
            services.AddViberClient(Configuration.GetSection("Tokens").GetSection("Viber").Value);
            
            services.AddSingleton<IAction<TelegramCommand>, StartAction>();
            services.AddSingleton<IAction<TelegramCommand>, GetAllCurrenciesAction>();
            services.AddSingleton<IAction<TelegramCommand>, GetCurrencyRateAction>();
            
            services.AddSingleton<IAction<ViberCommand>, BotCore.Viber.Test.Actions.StartAction>();
            
            services.AddSingleton<IBankService, BankService>();
            
            services.AddTelegramActionsExecutor();
            services.AddViberActionsExecutor();
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