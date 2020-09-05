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
            var commands = serviceProvider.GetServices<ICommand>();
            var commandExecutor = new CommandExecutor(commands);
            services.AddSingleton<ICommandExecutor>(commandExecutor);
        }
        
        public static void AddCommands(this IServiceCollection services)
        {
        }
    }
}