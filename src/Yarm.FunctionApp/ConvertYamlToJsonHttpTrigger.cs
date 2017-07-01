using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Yarm.Functions;
using Yarm.Functions.FunctionFactories;

namespace Yarm.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity for the ConvertYamlToJson event.
    /// </summary>
    public static class ConvertYamlToJsonHttpTrigger
    {
        /// <summary>
        /// Gets or sets the <see cref="Functions.FunctionFactories.FunctionFactory"/> instance.
        /// </summary>
        public static FunctionFactory FunctionFactory { get; set; } = new FunctionFactory();

        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ILogger log)
        {
            var res = await FunctionFactory.Create<IConvertYamlToJsonFunction>(log)
                                           .InvokeAsync(req)
                                           .ConfigureAwait(false);
            return res;
        }
    }
}