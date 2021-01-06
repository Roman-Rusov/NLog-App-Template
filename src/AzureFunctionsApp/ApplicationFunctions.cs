using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Apps.Common;
using Apps.Common.Logging;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Apps.AzureFunctionsApp
{
    public class ApplicationFunctions
    {
        private readonly ILogger<ApplicationFunctions> _logger;
        private readonly IApplication _application;

        public ApplicationFunctions(
            ILogger<ApplicationFunctions> logger,
            IApplication application)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _application = application ?? throw new ArgumentNullException(nameof(application));
        }

        [FunctionName("RunApplication")]
        public async Task RunApplication(
            [TimerTrigger("%Application_Schedule%"
#if DEBUG
                , RunOnStartup = true
#endif
                )] TimerInfo timer,
            CancellationToken ct = default)
        {
            // TODO:  Decouple execution and logging, add logging scope via AOP.
            using var applicationRunLoggingScope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["Application.Name"] = _application.Name,
                ["Application.Run.EnvironmentType"] = "AzureFunctions",
                ["Application.Run.Id"] = _application.Name,
            });
            
            try
            {
                _logger.LogInformation("Running the application.");

                await _application.Run(ct);

                _logger.LogInformation("Application run has successfully completed.");
            }
            catch (Exception ex) when (
                _logger.LogErrorEvent(ex, "An error occurred during the application run."))
            {
                // TODO:  Do a recovery job if needed.
            }
        }
    }
}