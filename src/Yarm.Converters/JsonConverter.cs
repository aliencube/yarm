using System.Dynamic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using YamlDotNet.Serialization;

namespace Yarm.Converters
{
    /// <summary>
    /// This represents the converter entity from JSON to YAML.
    /// </summary>
    public static class JsonConverter
    {
        /// <summary>
        /// Converts the JSON string to YAML string.
        /// </summary>
        /// <param name="json">JSON string.</param>
        /// <returns>Returns YAML string converted.</returns>
        public static string ConvertToYaml(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            var converter = new ExpandoObjectConverter();
            var deserialised = JsonConvert.DeserializeObject<ExpandoObject>(json, converter);
            var serialised = new SerializerBuilder().Build().Serialize(deserialised);

            return serialised;
        }
    }
}
