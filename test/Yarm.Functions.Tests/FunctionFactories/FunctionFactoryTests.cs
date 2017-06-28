using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

using Yarm.Extensions;
using Yarm.Functions.FunctionFactories;
using Yarm.Functions.ServiceLocators;
using Yarm.Functions.Tests.Fixtures;

namespace Yarm.Functions.Tests.FunctionFactories
{
    /// <summary>
    /// This represents the test entity for the <see cref="FunctionFactory"/> class.
    /// </summary>
    public class FunctionFactoryTests
    {
        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public void Given_That_Function_ShouldBe_Created()
        {
            var handler = new RegistrationHandler()
                              {
                                  RegisterTypeAsInstancePerDependency = p => p.RegisterTypeAsInstancePerDependency<FooFunction, IFooFunction>()
                              };
            var factory = new FunctionFactory(handler);

            var logger = new Mock<ILogger>();
            var function = factory.Create<IFooFunction>(logger.Object);

            function.Log.Should().NotBeNull();
            function.ServiceLocator.Should().NotBeNull();
        }
    }
}
