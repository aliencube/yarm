using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Yarm.Converters;
using Yarm.Functions.FunctionFactories;

namespace Yarm.Functions
{
    /// <summary>
    /// This represents the function entity that converts YAML to JSON.
    /// </summary>
    public class ConvertYamlToJsonFunction : FunctionBase, IConvertYamlToJsonFunction
    {
        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public override async Task<HttpResponseMessage> InvokeAsync(HttpRequestMessage req)
        {
            var containsPayload = await this.ContainsPayloadAsync(req).ConfigureAwait(false);
            if (!containsPayload)
            {
                this.LogError(ResponseMessages.RequestPayloadNotFound);

                return this.CreateBadRequestResponse(req, ResponseMessages.RequestPayloadNotFound);
            }

            if (!this.HasValidContentType(req))
            {
                this.LogError(ResponseMessages.InvalidRequestPayloadContentType);

                return this.CreateBadRequestResponse(req, ResponseMessages.InvalidRequestPayloadContentType);
            }

            var yaml = await req.Content.ReadAsStringAsync().ConfigureAwait(false);
            var json = YamlConverter.ConvertToJson(yaml);
            var jo = JObject.Parse(json);

            var res = this.CreateOkResponse(req, jo);

            return res;
        }
    }
}
