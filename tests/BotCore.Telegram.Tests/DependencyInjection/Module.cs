using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotCore.Telegram.Tests.DependencyInjection
{
    public abstract class Module : IModule
    {
        protected const string TestEnvName = "test";

        public abstract void Load(IServiceCollection collection, IConfiguration configuration);

        protected bool IsTestConfiguration(IConfiguration configuration)
        {
            var currentEnvName = configuration["Environment"] ?? string.Empty;
            return currentEnvName.Equals(TestEnvName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}