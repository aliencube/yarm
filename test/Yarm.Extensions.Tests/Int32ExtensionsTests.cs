using System;

using FluentAssertions;

using Xunit;

namespace Yarm.Extensions.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="Int32Extensions"/> class.
    /// </summary>
    public class Int32ExtensionsTests
    {
        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="comparer">Value to compare.</param>
        /// <param name="expected">Value to expect.</param>
        [Theory]
        [InlineData(0, 1, true)]
        [InlineData(1, 0, false)]
        public void Given_Values_IsLessThanOrEqualTo_ShouldReturn_Result(int value, int comparer, bool expected)
        {
            var result = Int32Extensions.IsLessThanOrEqualTo(value, comparer);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="value">Value to be compared.</param>
        /// <param name="comparer">Value to compare.</param>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        public void Given_InvalidValue_ThrowIfLessThanOrEqualTo_ShouldThrow_Exception(int value, int comparer)
        {
            Action action = () => { var result = Int32Extensions.ThrowIfLessThanOrEqualTo(value, comparer); };
            action.ShouldThrow<ArgumentOutOfRangeException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="comparer">Comparer value.</param>
        /// <param name="expected">Value expected.</param>
        [Theory]
        [InlineData(1, 0,  1)]
        public void Given_ValidValue_ThrowIfLessThanOrEqualTo_ShouldReturn_Result(int value, int comparer, int expected)
        {
            var result = Int32Extensions.ThrowIfLessThanOrEqualTo(value, comparer);

            result.Should().Be(expected);
        }
    }
}