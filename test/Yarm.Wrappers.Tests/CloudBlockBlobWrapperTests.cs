using System;

using FluentAssertions;

using Xunit;

namespace Yarm.Wrappers.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="CloudBlockBlobWrapper"/> class.
    /// </summary>
    public class CloudBlockBlobWrapperTests
    {
        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => new CloudBlockBlobWrapper(null);
            action.ShouldThrow<ArgumentNullException>();
        }
    }
}