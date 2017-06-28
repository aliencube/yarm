using System;

using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

using Yarm.Functions.Tests.Extensions;

using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Yarm.Functions.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="LoggerExtensionsTests"/> class.
    /// </summary>
    public class LoggerExtensionsFixture : IDisposable
    {
        private ILoggerProvider _provider;
        private bool _disposed;

        /// <summary>
        /// Arranges the <see cref="ILogger"/> instance.
        /// </summary>
        /// <param name="logLevel"><see cref="LogEventLevel"/> value.</param>
        /// <param name="sink"><see cref="TestSink"/> instance.</param>
        /// <returns>Returns the <see cref="ILogger"/> instance.</returns>
        public ILogger ArrangeLogger(LogEventLevel logLevel, out TestSink sink)
        {
            sink = new TestSink();
            var config = new LoggerConfiguration()
                             .WriteTo.Sink(sink)
                             .MinimumLevel.Is(logLevel);
            this._provider = new SerilogLoggerProvider(config.CreateLogger());
            var logger = this._provider.CreateLogger(this.GetType().FullName);

            return logger;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._provider.Dispose();

            this._disposed = true;
        }
    }
}
