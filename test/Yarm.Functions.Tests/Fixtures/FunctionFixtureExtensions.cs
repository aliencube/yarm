using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;

using Moq;

using Yarm.Functions.FunctionFactories;

namespace Yarm.Functions.Tests.Fixtures
{
    /// <summary>
    /// This represents the extension entity for the <see cref="IFunction"/> fixtures.
    /// </summary>
    public static class FunctionFixtureExtensions
    {
        /// <summary>
        /// Sets the <see cref="Mock{ILogger}"/> instance.
        /// </summary>
        /// <param name="function"><see cref="IFunction"/> instance.</param>
        /// <param name="log"><see cref="Mock{ILogger}"/> instance.</param>
        /// <returns>Returns the <see cref="IFunction"/> instance.</returns>
        public static IFunction SetLoggerToFixture(this IFunction function, Mock<ILogger> log)
        {
            function.Log = log.Object;

            return function;
        }

        /// <summary>
        /// Sets the <see cref="Mock{IServiceLocator}"/> instance.
        /// </summary>
        /// <param name="function"><see cref="IFunction"/> instance.</param>
        /// <param name="locator"><see cref="Mock{IServiceLocator}"/> instance.</param>
        /// <returns>Returns the <see cref="IFunction"/> instance.</returns>
        public static IFunction SetServiceLocatorToFixture(this IFunction function, Mock<IServiceLocator> locator)
        {
            function.ServiceLocator = locator.Object;

            return function;
        }
    }
}