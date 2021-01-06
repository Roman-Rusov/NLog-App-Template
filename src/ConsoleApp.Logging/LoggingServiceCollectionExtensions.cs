using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace Apps.ConsoleApp.Logging
{
    public static class LoggingServiceCollectionExtensions
    {
        public static IServiceCollection AddNLog(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddLogging(loggingBuilder => loggingBuilder
               .AddConfiguration(configuration.GetSection("Logging"))
               .AddNLog(new NLogProviderOptions
                {
                    CaptureMessageProperties = true,
                    CaptureMessageTemplates = true,
                    IncludeActivtyIdsWithBeginScope = true,
                    IncludeScopes = true
                }));
    }
}