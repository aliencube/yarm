using System;
using System.Net.Http.Formatting;

using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;

using Moq;

using Yarm.Functions.FunctionFactories;
using Yarm.Models.Settings;
using Yarm.Services;

namespace Yarm.Functions.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for function test classes.
    /// </summary>
    public class FunctionFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionFixture"/> class.
        /// </summary>
        public FunctionFixture()
        {
            this.LogMessage = new LogMessage();

            this.Log = new Mock<ILogger>();
            this.Log.Setup(p => p.Log<object>(LogLevel.Trace, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback((LogLevel l, EventId e, object o, Exception ex, Func<object, Exception, string> f) => this.LogMessage.Trace = o.ToString());
            this.Log.Setup(p => p.Log<object>(LogLevel.Debug, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback((LogLevel l, EventId e, object o, Exception ex, Func<object, Exception, string> f) => this.LogMessage.Debug = o.ToString());
            this.Log.Setup(p => p.Log<object>(LogLevel.Information, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback((LogLevel l, EventId e, object o, Exception ex, Func<object, Exception, string> f) => this.LogMessage.Information = o.ToString());
            this.Log.Setup(p => p.Log<object>(LogLevel.Warning, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback((LogLevel l, EventId e, object o, Exception ex, Func<object, Exception, string> f) => this.LogMessage.Warning = o.ToString());
            this.Log.Setup(p => p.Log<object>(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback((LogLevel l, EventId e, object o, Exception ex, Func<object, Exception, string> f) => this.LogMessage.Error = o.ToString());
            this.Log.Setup(p => p.Log<object>(LogLevel.Critical, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback((LogLevel l, EventId e, object o, Exception ex, Func<object, Exception, string> f) => this.LogMessage.Critical = o.ToString());

            this.ServiceLocator = new Mock<IServiceLocator>();
            this.ServiceLocator.Setup(p => p.GetInstance<MediaTypeFormatter>()).Returns(new JsonMediaTypeFormatter());

            this.FunctionAppSettings = new Mock<IFunctionAppSettings>();
        }

        /// <summary>
        /// Gets the <see cref="Mock{ILogger}"/> instance.
        /// </summary>
        public Mock<ILogger> Log { get; }

        /// <summary>
        /// Gets or sets the log error message.
        /// </summary>
        public LogMessage LogMessage { get; set; }

        /// <summary>
        /// Gets the <see cref="Mock{IServiceLocator}"/> instance.
        /// </summary>
        public Mock<IServiceLocator> ServiceLocator { get; }

        /// <summary>
        /// Gets or sets the <see cref="IFunction"/> instance.
        /// </summary>
        public IFunction Function { get; protected set; }

        /// <summary>
        /// Gets the <see cref="Mock{IFunctionAppSettings}"/> instance.
        /// </summary>
        public Mock<IFunctionAppSettings> FunctionAppSettings { get; }

        /// <summary>
        /// Arranges the <see cref="IGetArmTemplateDirectoriesFunction"/> instance.
        /// </summary>
        /// <param name="gitHubService"><see cref="Mock{IGitHubService}"/> instance.</param>
        /// <returns>Returns the <see cref="IGetArmTemplateDirectoriesFunction"/> instance.</returns>
        public IGetArmTemplateDirectoriesFunction ArrangeGetArmTemplateDirectoriesFunction(out Mock<IGitHubService> gitHubService)
        {
            gitHubService = new Mock<IGitHubService>();

            var function = new GetArmTemplateDirectoriesFunction(gitHubService.Object)
                               .SetLoggerToFixture(this.Log)
                               .SetServiceLocatorToFixture(this.ServiceLocator);

            return function as GetArmTemplateDirectoriesFunction;
        }

        /// <summary>
        /// Arranges the <see cref="IGetArmTemplateDirectoriesFunction"/> instance.
        /// </summary>
        /// <param name="gitHubService"><see cref="Mock{IGitHubService}"/> instance.</param>
        /// <returns>Returns the <see cref="IGetArmTemplateDirectoriesFunction"/> instance.</returns>
        public IGetArmTemplateFunction ArrangeGetArmTemplateFunction(out Mock<IGitHubService> gitHubService)
        {
            gitHubService = new Mock<IGitHubService>();

            var function = new GetArmTemplateFunction(gitHubService.Object)
                               .SetLoggerToFixture(this.Log)
                               .SetServiceLocatorToFixture(this.ServiceLocator);

            return function as GetArmTemplateFunction;
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

            this._disposed = true;
        }
    }
}
