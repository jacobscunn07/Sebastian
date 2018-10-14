using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sebastian.Api;

namespace Sebastian.Tests
{
    public static class Testing
    {
        private static IServiceScope ServiceScope => TestServiceScope.CurrentScope;

        public static T Resolve<T>()
        {
            return ServiceScope.ServiceProvider.GetService<T>();
        }
    }
}