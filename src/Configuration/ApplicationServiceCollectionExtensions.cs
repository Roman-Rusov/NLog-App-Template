using System.IO;

using Microsoft.Extensions.Configuration;

namespace Apps.Configuration
{
    public static class ApplicationConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddApplicationSettings(
            this IConfigurationBuilder builder,
            string appRootPath,
            string environmentType,
            string environmentName = null) =>
            builder
               .AddJsonFile(
                    Path.Combine(appRootPath, "appsettings.json"),
                    optional: false,
                    reloadOnChange: false)
               .AddJsonFile(
                    Path.Combine(appRootPath, $"appsettings.{environmentType}.json"),
                    optional: false,
                    reloadOnChange: false)
               .AddJsonFile(
                    Path.Combine(appRootPath, $"appsettings.{environmentType}.{environmentName}.json"),
                    optional: environmentName == null,
                    reloadOnChange: false);
    }
}