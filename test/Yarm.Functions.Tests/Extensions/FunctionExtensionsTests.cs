using System;

using FluentAssertions;

using Xunit;

using Yarm.Functions.Extensions;
using Yarm.Functions.Tests.Fixtures;

namespace Yarm.Functions.Tests.Extensions
{
    /// <summary>
    /// This represents the test entity for the <see cref="FunctionExtensions"/> class.
    /// </summary>
    public class FunctionExtensionsTests
    {
        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_LoadProperty_ShouldThrow_Exception()
        {
            var instance = new FooFunction();

            Action action = () => FunctionExtensions.LoadProperty((IFooFunction)null, null);
            action.ShouldThrow<ArgumentNullException>();

            action = () => FunctionExtensions.LoadProperty(instance, null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="bar">Bar value.</param>
        [Theory]
        [InlineData("hello world")]
        public void Given_Action_LoadProperty_ShouldReturn_Result(string bar)
        {
            var instance = new FooFunction();

            var result = FunctionExtensions.LoadProperty(instance, p => p.Bar = bar);

            result.Bar.Should().BeEquivalentTo(bar);
        }
    }
}
