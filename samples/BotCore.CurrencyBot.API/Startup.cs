using System;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.Interfaces;
using BotCore.CurrencyBot.Infrastructure.Data;
using BotCore.CurrencyBot.Infrastructure.Middlewares;
using BotCore.CurrencyBot.Infrastructure.Services;
using BotCore.Telegram;
using BotCore.Telegram.CurrencyBot.Actions;
using BotCore.Telegram.DomainModels;
using BotCore.Viber;
using BotCore.Viber.DomainModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BotCore.CurrencyBot.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var isProd = CurrentEnvironment.IsProduction();
            services.AddControllers().AddNewtonsoftJson();

            services.AddTelegramClient(Configuration, isProd);
            services.AddViberClient(Configuration, isProd);

            services.AddScoped<IAction<TelegramCommand>, ConvertCurrencyAction>();
            services.AddScoped<IAction<TelegramCommand>, GetAllCurrenciesAction>();
            services.AddScoped<IAction<TelegramCommand>, GetConcreteCurrencyRateAction>();
            services.AddScoped<IAction<TelegramCommand>, GetCurrencyRateAction>();
            services.AddScoped<IAction<TelegramCommand>, GetCurrencyRateChartAction>();
            services.AddScoped<IAction<TelegramCommand>, GetDefaultKeyboardAction>();
            services.AddScoped<IAction<TelegramCommand>, SetupDefaultCurrenciesAction>();
            services.AddScoped<IAction<TelegramCommand>, InternalServerErrorAction>();
            services.AddScoped<IAction<TelegramCommand>, StartAction>();


            services.AddScoped<IAction<ViberCommand>, Viber.CurrencyBot.Actions.GetAllCurrenciesAction>();
            services.AddScoped<IAction<ViberCommand>, Viber.CurrencyBot.Actions.GetCurrencyRateAction>();
            services.AddScoped<IAction<ViberCommand>, Viber.CurrencyBot.Actions.StartAction>();

            services.AddScoped<IApiProvider, ApiProvider>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IDbCacheService, DbCacheService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPlotService, PlotService>();
            services.AddScoped<IUsersService, UsersService>();

            if (isProd)
                services.AddDbContext<BotCoreTestContext>(options =>
                    options.UseNpgsql(Environment.GetEnvironmentVariable("BotCoreDbConnectionString")));
            else
                services.AddDbContext<BotCoreTestContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("BotCoreTestContext")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericRepository<>));

            services.AddTelegramActionsExecutor();
            services.AddViberActionsExecutor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}