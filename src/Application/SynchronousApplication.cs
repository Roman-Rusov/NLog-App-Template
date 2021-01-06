using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Apps.Application.Contracts;
using Apps.Common;
using Apps.Common.Logging;

using Microsoft.Extensions.Logging;

namespace Apps.Application
{
    public abstract class SynchronousApplication : IApplication
    {
        private readonly ILogger<SynchronousApplication> _logger;
        private readonly IReadOnlyCollection<IApplicationTask> _appTasks;

        protected SynchronousApplication(
            ILogger<SynchronousApplication> logger,
            IEnumerable<IApplicationTask> appTasks)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appTasks = appTasks?.ToArray() ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Run(CancellationToken ct = default)
        {
            using var appRunLoggingScope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["Application.Name"] = (this as IApplication).Name,
                ["Application.RunId"] = Guid.NewGuid()
            });

            _logger.LogInformation(
                "{Application.Tasks.Count} apps resolved for running.",
                _appTasks.Count);

            foreach (var app in _appTasks)
            {
                using var appTaskRunLoggingScope = _logger.BeginScope(new Dictionary<string, object>
                {
                    ["Application.Task.Name"] = app.Name
                });

                try
                {
                    _logger.LogInformation("Running the application task.");

                    await app.Run(ct);

                    _logger.LogInformation("Application task run has successfully completed.");
                }
                catch (Exception ex) when (
                    _logger.LogErrorEvent(ex, "An error occurred during the application task run."))
                {
                    // Suppress all errors.
                }
            }

            _logger.LogInformation(
                "All {Application.Tasks.Count} application tasks have been run.",
                _appTasks.Count);
        }
    }
}