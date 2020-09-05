using Microsoft.Extensions.DependencyInjection;
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
    }
}