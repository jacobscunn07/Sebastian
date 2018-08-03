using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sebastian.Api;

namespace Sebastian.Tests
{
    public static class Testing
    {
        private static readonly IServiceScopeFactory ScopeFactory;
        public static IConfigurationRoot Configuration { get; }
        
        static Testing()
        {
            Configuration = new ConfigurationBuilder().Build();

            var app = new Startup(Configuration);
            var serviceCollection = new ServiceCollection();
            
            app.ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            ScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
        }

        public static void Action(Action<IServiceProvider> action)
        {
            using (var scope = ScopeFactory.CreateScope())
            {
                try
                {
                    action(scope.ServiceProvider);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}