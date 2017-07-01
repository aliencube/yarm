using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Yarm.Converters;
using Yarm.Functions.FunctionFactories;

namespace Yarm.Functions
{
    /// <summary>
    /// This represents the function entity that converts JSON to YAML.
    /// </summary>
    public class ConvertJsonToYamlFunction : FunctionBase, IConvertJsonToYamlFunction
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

            var json = await req.Content.ReadAsStringAsync().ConfigureAwait(false);
            var yaml = JsonConverter.ConvertToYaml(json);

            var res = this.CreateOkResponse(req, yaml, "application/yaml");

            return res;
        }
    }
}
