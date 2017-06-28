using System;
using System.Collections.Generic;

using FluentAssertions;

using Xunit;

namespace Yarm.Extensions.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="StringExtensions"/> class.
    /// </summary>
    public class StringExtensionsTests
    {
        /// <summary>
        /// Tests the method whether to return <c>True</c> or not.
        /// </summary>
        [Fact]
        public void Given_Null_IsNullOrWhiteSpace_ShouldReturn_True()
        {
            var result = StringExtensions.IsNullOrWhiteSpace(null);
            result.Should().BeTrue();

            result = StringExtensions.IsNullOrWhiteSpace(string.Empty);
            result.Should().BeTrue();

            result = StringExtensions.IsNullOrWhiteSpace("\t");
            result.Should().BeTrue();
        }

        /// <summary>
        /// Tests the method whether to return <c>False</c> or not.
        /// </summary>
        [Fact]
        public void Given_Value_IsNullOrWhiteSpace_ShouldReturn_False()
        {
            var result = StringExtensions.IsNullOrWhiteSpace("x");
            result.Should().BeFalse();
        }

        /// <summary>
        /// Tests the method whether to throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_Value_ThrowIfNullOrWhiteSpace_ShouldThrow_Exception()
        {
            Action action = () => { var result = StringExtensions.ThrowIfNullOrWhiteSpace(null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests the method whether to return <c>True</c> or not.
        /// </summary>
        /// <param name="value">Value to be compared.</param>
        /// <param name="comparer">Value to compare.</param>
        [Theory]
        [InlineData("hello world", "HELLO WORLD")]
        [InlineData("hello world", "HEllO WorLd")]
        [InlineData("HELLO WORLD", "hello world")]
        public void Given_Value_IsEquivalentTo_ShouldReturn_True(string value, string comparer)
        {
            var result = StringExtensions.IsEquivalentTo(value, comparer);
            result.Should().BeTrue();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Given_NullValue_ToInt32_ShouldThrow_Exception(string value)
        {
            Action action = () => { var result = StringExtensions.ToInt32(value); };
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        [Theory]
        [InlineData("hello")]
        public void Given_StringValue_ToInt32_ShouldThrow_Exception(string value)
        {
            Action action = () => { var result = StringExtensions.ToInt32(value); };
            action.ShouldThrow<FormatException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        [Theory]
        [InlineData("123456789012345")]
        public void Given_BigValue_ToInt32_ShouldThrow_Exception(string value)
        {
            Action action = () => { var result = StringExtensions.ToInt32(value); };
            action.ShouldThrow<OverflowException>();
        }

        /// <summary>
        /// Tests whether the method should return a result or not.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="expected">Value to be expected.</param>
        [Theory]
        [InlineData("12345", 12345)]
        public void Given_Value_ToInt32_ShouldReturn_Result(string value, int expected)
        {
            var result = StringExtensions.ToInt32(value);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_Null_ToBoolean_ShouldThrow_Exception()
        {
            Action action = () => { var result = StringExtensions.ToBoolean("hello"); };
            action.ShouldThrow<FormatException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="expected">Value to check.</param>
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("True", true)]
        [InlineData("true", true)]
        [InlineData("False", false)]
        [InlineData("false", false)]
        public void Given_Value_ToBoolean_ShouldReturn_Result(string value, bool expected)
        {
            var result = StringExtensions.ToBoolean(value);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <param name="expected">Value to be expected.</param>
        [Theory]
        [InlineData("http://localhost:8080", "http%3A%2F%2Flocalhost%3A8080")]
        [InlineData("hello world", "hello%20world")]
        [InlineData("?hello='wo_rld'", "%3Fhello%3D%27wo_rld%27")]
        [InlineData("&lorem=\"ip-sum\"", "%26lorem%3D%22ip-sum%22")]
        public void Given_Value_UrlEncode_ShouldReturn_Result(string value, string expected)
        {
            var result = StringExtensions.UrlEncode(value);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <param name="expected">Value to be expected.</param>
        [Theory]
        [InlineData("http%3A%2F%2Flocalhost%3A8080", "http://localhost:8080")]
        [InlineData("hello%20world", "hello world")]
        [InlineData("%3Fhello%3D%27wo_rld%27", "?hello='wo_rld'")]
        [InlineData("%26lorem%3D%22ip-sum%22", "&lorem=\"ip-sum\"")]
        public void Given_Value_UrlDecode_ShouldReturn_Result(string value, string expected)
        {
            var result = StringExtensions.UrlDecode(value);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_ContainsEquivalent_ShouldThrow_Exception()
        {
            Action action = () => { var result = StringExtensions.ContainsEquivalent((string)null, "value"); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var result = StringExtensions.ContainsEquivalent((IEnumerable<string>)null, "value"); };
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="expected">Expected result.</param>
        /// <param name="value">Value to check.</param>
        /// <param name="comparer">Value to compare.</param>
        [Theory]
        [InlineData(true, "hello", "el")]
        [InlineData(true, "world", "wo")]
        [InlineData(false, "hello", "wo")]
        [InlineData(false, "world", "lo")]
        public void Given_ParameterValues_ContainsEquivalent_ShouldReturn_Result(bool expected, string value, string comparer)
        {
            var result = StringExtensions.ContainsEquivalent(value, comparer);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="expected">Expected result.</param>
        /// <param name="item">Item to check.</param>
        /// <param name="items">List of items.</param>
        [Theory]
        [InlineData(true, "hello", "hello", "world")]
        [InlineData(true, "world", "hello", "world")]
        [InlineData(false, "hello", "world")]
        [InlineData(false, "world", "lorem", "ipsum")]
        public void Given_ParameterLists_ContainsEquivalent_ShouldReturn_Result(bool expected, string item, params string[] items)
        {
            var result = StringExtensions.ContainsEquivalent(items, item);
            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_StartsWithEquivalent_ShouldThrow_Exception()
        {
            var comparer = "abc";

            Action action = () => StringExtensions.StartsWithEquivalent(null, comparer);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return <c>False</c> or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_StartsWithEquivalent_ShouldReturn_False()
        {
            var value = "abc";

            var result = StringExtensions.StartsWithEquivalent(value, null);

            result.Should().BeFalse();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="expected">Expected result.</param>
        /// <param name="value">Value to check.</param>
        /// <param name="comparer">Value to compare.</param>
        [Theory]
        [InlineData(true, "Hello", "h")]
        [InlineData(true, "hello", "H")]
        [InlineData(false, "Hello", "e")]
        [InlineData(false, "Hello", "E")]
        public void Given_Parameters_StartsWithEquivalent_ShouldReturn_Result(bool expected, string value, string comparer)
        {
            var result = StringExtensions.StartsWithEquivalent(value, comparer);

            result.Should().Be(expected);
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_EndsWithEquivalent_ShouldThrow_Exception()
        {
            var comparer = "abc";

            Action action = () => StringExtensions.EndsWithEquivalent(null, comparer);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return <c>False</c> or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_EndsWithEquivalent_ShouldReturn_False()
        {
            var value = "abc";

            var result = StringExtensions.EndsWithEquivalent(value, null);

            result.Should().BeFalse();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="expected">Expected result.</param>
        /// <param name="value">Value to check.</param>
        /// <param name="comparer">Value to compare.</param>
        [Theory]
        [InlineData(true, "Hello", "O")]
        [InlineData(true, "hellO", "o")]
        [InlineData(false, "Hello", "e")]
        [InlineData(false, "Hello", "E")]
        public void Given_Parameters_EndsWithEquivalent_ShouldReturn_Result(bool expected, string value, string comparer)
        {
            var result = StringExtensions.EndsWithEquivalent(value, comparer);

            result.Should().Be(expected);
        }
    }
}