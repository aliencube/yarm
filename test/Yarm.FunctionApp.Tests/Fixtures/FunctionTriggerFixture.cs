using System;

using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;

using Moq;

using Yarm.Functions.FunctionFactories;

namespace Yarm.FunctionApp.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for Function app testing.
    /// </summary>
    public class FunctionTriggerFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionTriggerFixture"/> class.
        /// </summary>
        public FunctionTriggerFixture()
        {
            this.Log = new Mock<ILogger>();
        }

        /// <summary>
        /// Gets the <see cref="Mock{ILogger}"/> instance.
        /// </summary>
        public Mock<ILogger> Log { get; }

        /// <summary>
        /// Gets the <see cref="Mock{FunctionFactory}"/> instance.
        /// </summary>
        /// <typeparam name="TFunction">Type of function inheriting the <see cref="IFunction"/> interface.</typeparam>
        /// <param name="function"><see cref="Mock{TFunction}"/> instance.</param>
        /// <returns>Returns the <see cref="Mock{FunctionFactory}"/> instance.</returns>
        public Mock<FunctionFactory> GetFunctionFactory<TFunction>(out Mock<TFunction> function)
            where TFunction : class, IFunction
        {
            function = new Mock<TFunction>();

            var locator = new Mock<IServiceLocator>();
            locator.Setup(p => p.GetInstance<TFunction>()).Returns(function.Object);

            var factory = new Mock<FunctionFactory>();
            factory.SetupGet(p => p.ServiceLocator).Returns(locator.Object);
            factory.Setup(p => p.Create<TFunction>(It.IsAny<ILogger>())).Returns(function.Object);

            return factory;
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
