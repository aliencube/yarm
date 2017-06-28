namespace Yarm.Functions.Tests.Fixtures
{
    /// <summary>
    /// This represents the log message entity for testing.
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// Gets or sets the trace message.
        /// </summary>
        public string Trace { get; set; }

        /// <summary>
        /// Gets or sets the debug message.
        /// </summary>
        public string Debug { get; set; }

        /// <summary>
        /// Gets or sets the information message.
        /// </summary>
        public string Information { get; set; }

        /// <summary>
        /// Gets or sets the warning message.
        /// </summary>
        public string Warning { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the critical message.
        /// </summary>
        public string Critical { get; set; }
    }
}