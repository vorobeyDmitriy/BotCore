using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BotCore.Tests
{
    [TestFixture]
    public abstract class TestClassBase
    {
        [SetUp]
        protected virtual void TestSetup()
        {
        }

        [TearDown]
        protected virtual void TestDown()
        {
        }

        protected virtual IModule Module { get; }
        protected IServiceProvider AppContainer { get; set; }
        protected IConfiguration Configuration { get; set; }

        [OneTimeSetUp]
        protected virtual void Setup()
        {
            var builder = new ServiceCollection();
            // Set up configuration sources.
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("Configs/appsettings.json", false)
                .AddJsonFile("Configs/servicesettings.json", true)
                .AddInMemoryCollection(new Dictionary<string, string>());

            Configuration = configurationBuilder.Build();

            builder.AddSingleton(Configuration);
            RegisterIoCModules(builder);
            AppContainer = builder.BuildServiceProvider();
        }

        [OneTimeTearDown]
        protected virtual void Down()
        {
        }

        protected virtual void RegisterIoCModules(IServiceCollection collection)
        {
            Module?.Load(collection, Configuration);
        }
        
        protected T GetService<T>()
        {
            return AppContainer.GetService<T>();
        }
    }
}