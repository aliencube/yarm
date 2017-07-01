using System.Net.Http;
using System.Threading.Tasks;

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
        public override Task<HttpResponseMessage> InvokeAsync(HttpRequestMessage req)
        {
            throw new System.NotImplementedException();
        }
    }
}
