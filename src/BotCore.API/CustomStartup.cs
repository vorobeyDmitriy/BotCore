using BotCore.Telegram.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBotCore.Core.Interfaces;
using TelegramBotCore.Core.Services;

namespace TelegramBotCore.API
{
    public static class CustomStartup
    {
        public static void AddCommandExecutor(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var commands = serviceProvider.GetServices<IAction>();
            var commandExecutor = new ActionExecutor(commands);
            services.AddSingleton<IActionExecutor>(commandExecutor);
        }
        
        public static void AddActions(this IServiceCollection services)
        {
        }

        public static void AddBotServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMessageSender, TelegramMessageSender>();
            
            var telegramBotClient = new TelegramBotClient(configuration.GetSection("TelegramBotToken").Value);
            services.AddSingleton<ITelegramBotClient>(telegramBotClient);
        }
    }
}