using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Apps.Common;
using Apps.Common.Logging;

using Microsoft.Extensions.Logging;

namespace Apps.ConsoleApp
{
    public class ConsoleApp
    {
        private readonly ILogger<ConsoleApp> _logger;
        private readonly IApplication _application;

        public ConsoleApp(
            ILogger<ConsoleApp> logger,
            IApplication application)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public async Task Run(CancellationToken ct = default)
        {
            var appName = _application.Name;

            // TODO:  Decouple execution and logging, add logging scope via AOP.
            using var applicationRunLoggingScope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["Application.Name"] = appName,
                ["Application.Run.EnvironmentType"] = "Console",
                ["Application.Run.Id"] = appName,
            });

            try
            {
                _logger.LogInformation("Running the {Application.Name} application.", appName);

                await _application.Run(ct);

                _logger.LogInformation("Run of the {Application.Name} has successfully completed.", appName);
            }
            catch (Exception ex) when (
                _logger.LogErrorEvent(ex, "An error occurred during the {Application.Name} application run.", appName))
            {
                // TODO:  Do a recovery job if needed.
            }
        }
    }
}