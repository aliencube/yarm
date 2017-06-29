using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace Yarm.Converters.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="JsonConverter"/> class.
    /// </summary>
    public class JsonConverterTests
    {
        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_ConvertToYaml_ShouldReturn_Null()
        {
            var result = JsonConverter.ConvertToYaml(null);
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
        public void Given_Json_ConvertToYaml_ShouldReturn_Result(string key1, string value1, string key2, int value2, string key3, bool value3)
        {
            var dic = new Dictionary<string, object>() { { key1, value1 }, { key2, value2 }, { key3, value3 } };
            var json = JsonConvert.SerializeObject(dic);

            var result = JsonConverter.ConvertToYaml(json);

            var lines = result.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var key in dic.Keys)
            {
                lines.Count(p => p.StartsWith(key)).Should().Be(1);
            }
        }
    }
}
