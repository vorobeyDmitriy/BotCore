using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotCore.Tests.DependencyInjection
{
    public abstract class Module : IModule
    {
        public abstract void Load(IServiceCollection collection, IConfiguration configuration);
    }
}