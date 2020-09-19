using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotCore.Telegram.Tests.DependencyInjection
{
    public interface IModule
    {
        void Load(IServiceCollection collection, IConfiguration configuration);
    }
}