using System.IO;

using Newtonsoft.Json;

using YamlDotNet.Serialization;

namespace Yarm.Converters
{
    /// <summary>
    /// This represents the converter entity from YAML to JSON.
    /// </summary>
    public static class YamlConverter
    {
        /// <summary>
        /// Converts the YAML string to JSON string.
        /// </summary>
        /// <param name="yaml">YAML string.</param>
        /// <returns>Returns JSON string converted.</returns>
        public static string ConvertToJson(string yaml)
        {
            if (string.IsNullOrWhiteSpace(yaml))
            {
                return null;
            }

            using (var reader = new StringReader(yaml))
            {
                var deserialised = new DeserializerBuilder().Build().Deserialize(reader);
                var serialised = JsonConvert.SerializeObject(deserialised);

                return serialised;
            }
        }
    }
}
