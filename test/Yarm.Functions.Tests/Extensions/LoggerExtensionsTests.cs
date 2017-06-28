using System;
using System.Linq;

using FluentAssertions;

using Serilog.Events;

using Xunit;

using Yarm.Functions.Tests.Fixtures;

using LoggerExtensions = Yarm.Functions.Extensions.LoggerExtensions;

namespace Yarm.Functions.Tests.Extensions
{
    /// <summary>
    /// This represents the test entity for the <see cref="LoggerExtensions"/> class.
    /// </summary>
    public class LoggerExtensionsTests : IClassFixture<LoggerExtensionsFixture>
    {
        private readonly LoggerExtensionsFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerExtensionsTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="LoggerExtensionsFixture"/> instance.</param>
        public LoggerExtensionsTests(LoggerExtensionsFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Info_ShouldThrow_Exception()
        {
            Action action = () => LoggerExtensions.Info(null, null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should work or not.
        /// </summary>
        /// <param name="logLevel"><see cref="LogEventLevel"/> value.</param>
        /// <param name="message">Log message.</param>
        [Theory]
        [InlineData(LogEventLevel.Information, "hello world")]
        public void Given_Message_Info_ShouldLog_Information(LogEventLevel logLevel, string message)
        {
            var logger = this._fixture.ArrangeLogger(logLevel, out TestSink sink);

            LoggerExtensions.Info(logger, message);

            sink.LogItems.Count.Should().Be(1);

            var log = sink.LogItems.Single();
            log.Level.Should().Be(logLevel);
            log.MessageTemplate.Text.Should().BeEquivalentTo(message);
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Warning_ShouldThrow_Exception()
        {
            Action action = () => LoggerExtensions.Warning(null, null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should work or not.
        /// </summary>
        /// <param name="logLevel"><see cref="LogEventLevel"/> value.</param>
        /// <param name="message">Log message.</param>
        [Theory]
        [InlineData(LogEventLevel.Warning, "hello world")]
        public void Given_Message_Warning_ShouldLog_Warning(LogEventLevel logLevel, string message)
        {
            var logger = this._fixture.ArrangeLogger(logLevel, out TestSink sink);

            LoggerExtensions.Warning(logger, message);

            sink.LogItems.Count.Should().Be(1);

            var log = sink.LogItems.Single();
            log.Level.Should().Be(logLevel);
            log.MessageTemplate.Text.Should().BeEquivalentTo(message);
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Error_ShouldThrow_Exception()
        {
            Action action = () => LoggerExtensions.Error(null, null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should work or not.
        /// </summary>
        /// <param name="logLevel"><see cref="LogEventLevel"/> value.</param>
        /// <param name="message">Log message.</param>
        [Theory]
        [InlineData(LogEventLevel.Error, "hello world")]
        public void Given_Message_Error_ShouldLog_Error(LogEventLevel logLevel, string message)
        {
            var logger = this._fixture.ArrangeLogger(logLevel, out TestSink sink);

            LoggerExtensions.Error(logger, message);

            sink.LogItems.Count.Should().Be(1);

            var log = sink.LogItems.Single();
            log.Level.Should().Be(logLevel);
            log.MessageTemplate.Text.Should().BeEquivalentTo(message);
        }
    }
}
