using Microsoft.Extensions.Logging;

using Yarm.Extensions;

namespace Yarm.Functions.Extensions
{
    /// <summary>
    /// This represents the extensions entity for the <see cref="ILogger"/>.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Writes an informational log message.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        /// <param name="message">Message to log.</param>
        public static void Info(this ILogger logger, string message)
        {
            logger.ThrowIfNullOrDefault();
            logger.LogInformation(message);
        }

        /// <summary>
        /// Writes a warning log message.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        /// <param name="message">Message to log.</param>
        public static void Warning(this ILogger logger, string message)
        {
            logger.ThrowIfNullOrDefault();
            logger.LogWarning(message);
        }

        /// <summary>
        /// Writes an error log message.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        /// <param name="message">Message to log.</param>
        public static void Error(this ILogger logger, string message)
        {
            logger.ThrowIfNullOrDefault();
            logger.LogError(message);
        }
    }
}
