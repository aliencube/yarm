using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace Yarm.Converters.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="YamlConverter"/> class.
    /// </summary>
    public class YamlConverterTests
    {
        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_ConvertToJson_ShouldReturn_Null()
        {
            var result = YamlConverter.ConvertToJson(null);
            result.Should().BeNullOrWhiteSpace();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="key1">Key 1.</param>
        /// <param name="value1">Value 1.</param>
        /// <param name="key2">Key 2.</param>
        /// <param name="value2">Value 2.</param>
        /// <param name="key3">Key 3.</param>
        /// <param name="value3">Value 3.</param>
        [Theory]
        [InlineData("key1", "value1", "key2", 2, "key3", true)]
        public void Given_Json_ConvertToJson_ShouldReturn_Result(string key1, string value1, string key2, int value2, string key3, bool value3)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{key1}: {value1}");
            sb.AppendLine($"{key2}: {value2}");
            sb.AppendLine($"{key3}: {value3}");

            var yaml = sb.ToString();

            var result = YamlConverter.ConvertToJson(yaml);

            var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
            dic.Keys.Should().Contain(key1);
            dic.Keys.Should().Contain(key2);
            dic.Keys.Should().Contain(key3);
        }
    }
}
