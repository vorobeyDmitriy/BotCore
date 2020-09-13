using BotCore.Core.Interfaces;
using BotCore.Core.Test.Interfaces;
using BotCore.Core.Test.Services;
using BotCore.Infrastructure.Test.Data;
using BotCore.Telegram;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Actions;
using BotCore.Viber;
using BotCore.Viber.DomainModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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

            services.AddTelegramClient(Configuration);
            services.AddViberClient(Configuration);

            services.AddScoped<IAction<TelegramCommand>, StartAction>();
            services.AddScoped<IAction<TelegramCommand>, GetAllCurrenciesAction>();
            services.AddScoped<IAction<TelegramCommand>, GetCurrencyRateAction>();


            services.AddScoped<IAction<ViberCommand>, Viber.Test.Actions.StartAction>();
            services.AddScoped<IAction<ViberCommand>, Viber.Test.Actions.GetAllCurrenciesAction>();
            services.AddScoped<IAction<ViberCommand>, Viber.Test.Actions.GetCurrencyRateAction>();

            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IMessageService, MessageService>();

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

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}