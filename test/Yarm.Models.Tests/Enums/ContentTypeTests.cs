using System.Linq;

using FluentAssertions;

using Xunit;

using Yarm.Models.Enums;

namespace Yarm.Models.Tests.Enums
{
    /// <summary>
    /// This represents the test entity for the <see cref="ContentType"/> class.
    /// </summary>
    public class ContentTypeTests
    {
        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public void Given_That_GetAll_ShouldReturn_Result()
        {
            var types = ContentType.GetAll().Select(p => p.ToString());
            types.Should().Contain(ContentType.Directory);
            types.Should().Contain(ContentType.File);
        }

        /// <summary>
        /// Tests whether the method should return <c>null</c> or not.
        /// </summary>
        /// <param name="name">Name to parse.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("hello")]
        public void Given_InvalidName_Parse_ShouldReturn_Null(string name)
        {
            var result = ContentType.Parse(name);
            result.Should().BeNull();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="name">Name to parse.</param>
        [Theory]
        [InlineData("file")]
        [InlineData("dir")]
        public void Given_Name_Parse_ShouldReturn_Result(string name)
        {
            var result = ContentType.Parse(name);
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Tests whether the method should return <c>null</c> or not.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        [Theory]
        [InlineData(0)]
        public void Given_InvalidValue_Parse_ShouldReturn_Null(int value)
        {
            var result = ContentType.Parse(value);
            result.Should().BeNull();
        }

        /// <summary>
        /// Tests whether the method should return <c>null</c> or not.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Given_Value_Parse_ShouldReturn_Result(int value)
        {
            var result = ContentType.Parse(value);
            result.Should().NotBeNull();
        }
    }
}
