using Apps.AzureFunctionsApp.Logging;
using Apps.CompositionRoot;
using Apps.Configuration;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Apps.AzureFunctionsApp.Startup))]

namespace Apps.AzureFunctionsApp
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            builder.ConfigurationBuilder
               .AddApplicationSettings(
                    context.ApplicationRootPath,
                    environmentType: "AzureFunctions",
                    context.EnvironmentName)
               .AddEnvironmentVariables();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();

            builder.Services
               .AddNLog(
                    context.Configuration,
                    context.ApplicationRootPath,
                    context.EnvironmentName)
               .AddApplication();
        }
    }
}