using System.IO;

using Apps.CompositionRoot;
using Apps.Configuration;
using Apps.ConsoleApp.Logging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apps.ConsoleApp
{
    public class Startup
    {
        private readonly string _appRootPath;
        private IConfiguration _configuration;

        public Startup()
        {
            var assemblyLocation = typeof(Startup).Assembly.Location;
            _appRootPath = Path.GetDirectoryName(assemblyLocation);
        }

        public void ConfigureAppConfiguration(IConfigurationBuilder builder) =>
            _configuration = builder
               .AddApplicationSettings(_appRootPath, environmentType: "Console")
               .Build();

        public IServiceCollection ConfigureServices(IServiceCollection services) =>
            services
               .AddSingleton(_configuration)
               .AddNLog(_configuration)
               .AddApplication()
               .AddTransient<ConsoleApp>();
    }
}