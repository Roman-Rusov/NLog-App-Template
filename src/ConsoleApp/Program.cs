using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apps.ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceProvider = GetServiceProvider();
            using var serviceScope = serviceProvider.CreateScope();

            var consoleApp = serviceScope.ServiceProvider.GetService<ConsoleApp>();
            await consoleApp.Run();
        }

        private static IServiceProvider GetServiceProvider()
        {
            var startup = new Startup();

            startup.ConfigureAppConfiguration(new ConfigurationBuilder());
            var services = startup.ConfigureServices(new ServiceCollection());

            return services.BuildServiceProvider();
        }
    }
}