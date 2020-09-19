using BotCore.Tests.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BotCore.Tests
{
    public abstract class TestModuleBase : Module
    {
        protected IConfiguration Configuration;

        public override void Load(IServiceCollection collection, IConfiguration configuration)
        {
            Configuration = configuration;
            RegisterDataComponents(collection);
            LoadHostableComponent(collection);
        }

        protected abstract void RegisterDataComponents(IServiceCollection services);

        protected abstract void LoadHostableComponent(IServiceCollection services);

        protected static void RegisterEmptyMock<TInterface>(IServiceCollection services) where TInterface : class
        {
            var mockObject = new Mock<TInterface>();
            services.AddSingleton(mockObject.Object);
        }
    }
}