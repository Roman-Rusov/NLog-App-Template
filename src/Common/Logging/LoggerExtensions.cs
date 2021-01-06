using System;

using Microsoft.Extensions.Logging;

namespace Apps.Common.Logging
{
    public static class LoggerExtensions
    {
        public static bool LogErrorEvent(
            this ILogger logger,
            Exception exception,
            string message,
            params object[] args)
        {
            logger.LogError(exception, message, args);
            return true;
        }

        public static bool LogCriticalEvent(
            this ILogger logger,
            Exception exception,
            string message,
            params object[] args)
        {
            logger.LogCritical(exception, message, args);
            return true;
        }
    }
}