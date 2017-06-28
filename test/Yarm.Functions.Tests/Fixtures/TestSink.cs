using System.Collections.Generic;

using Serilog.Core;
using Serilog.Events;

namespace Yarm.Functions.Tests.Fixtures
{
    /// <summary>
    /// This represents the Serilog sink entity.
    /// </summary>
    /// <remarks>Refer to: https://github.com/serilog/serilog-extensions-logging/blob/dev/test/Serilog.Extensions.Logging.Tests/Support/SerilogSink.cs</remarks>
    public class TestSink : ILogEventSink
    {
        /// <summary>
        /// Gets the list of <see cref="LogEvent"/> instances;
        /// </summary>
        public List<LogEvent> LogItems { get; } = new List<LogEvent>();

        /// <inheritdoc/>
        public void Emit(LogEvent logEvent)
        {
            this.LogItems.Add(logEvent);
        }
    }
}