using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Yarm.Functions;
using Yarm.Functions.Extensions;
using Yarm.Functions.FunctionFactories;

namespace Yarm.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity for the GetArmTemplate event.
    /// </summary>
    public static class GetArmTemplateHttpTrigger
    {
        /// <summary>
        /// Gets or sets the <see cref="Functions.FunctionFactories.FunctionFactory"/> instance.
        /// </summary>
        public static FunctionFactory FunctionFactory { get; set; } = new FunctionFactory();

        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="templateName">ARM template name.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, string templateName, ILogger log)
        {
            var res = await FunctionFactory.Create<IGetArmTemplateFunction>(log)
                                           .LoadProperty(p => p.TemplateName = templateName)
                                           .InvokeAsync(req)
                                           .ConfigureAwait(false);
            return res;
        }
    }
}