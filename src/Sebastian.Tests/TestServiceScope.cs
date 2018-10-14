using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sebastian.Api;
using System;

namespace Sebastian.Tests
{
    public static class TestServiceScope
    {
        private static IServiceScope _serviceScope;

        public static void Begin()
        {
            if (_serviceScope != null)
                throw new Exception("Cannot begin test service scope. Another service scope is still in effect.");

            var serviceScopeFactory = GetServiceScopeFactory();
            _serviceScope = serviceScopeFactory.CreateScope();
        }

        public static void End()
        {
            if (_serviceScope == null)
                throw new Exception("Cannot end test service scope. There is no service scope in effect.");

            _serviceScope.Dispose();
            _serviceScope = null;
        }

        public static IServiceScope CurrentScope
        {
            get
            {
                if (_serviceScope == null)
                    throw new Exception($"Cannot access the {nameof(CurrentScope)}. There is no scope in effect.");

                return _serviceScope;
            }
        }

        private static IServiceScopeFactory GetServiceScopeFactory()
        {
            var configuration = new ConfigurationBuilder().Build();
            var app = new Startup(configuration);
            var serviceCollection = new ServiceCollection();

            app.ConfigureServices(serviceCollection);

            return serviceCollection.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }
    }
}
