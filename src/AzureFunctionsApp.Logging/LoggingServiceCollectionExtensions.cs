using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Logging;

namespace Apps.AzureFunctionsApp.Logging
{
    public static class LoggingServiceCollectionExtensions
    {
        /*private static Logger _nLogger;*/

        public static IServiceCollection AddNLog(
            this IServiceCollection services,
            IConfiguration configuration,
            string applicationRootPath,
            string environmentName)
        {
            var configFilePath = Path.Combine(
                applicationRootPath,
                $"NLog.{environmentName}.config");

            /*_nLogger =*/ LogManager.Setup()
               .SetupExtensions(e => e.AutoLoadAssemblies(false))
               .LoadConfigurationFromFile(configFilePath, optional: false)
               .LoadConfiguration(builder => builder.LogFactory.AutoShutdown = false)
               /*.GetCurrentClassLogger()*/;

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                   .AddConfiguration(configuration.GetSection("Logging"))
                   .AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageProperties = true,
                        CaptureMessageTemplates = true,
                        IncludeActivtyIdsWithBeginScope = true,
                        IncludeScopes = true,
                        ShutdownOnDispose = true
                    });
            });

            return services;
        }
    }
}