using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;

namespace Yarm.Functions.FunctionFactories
{
    /// <summary>
    /// This provides interfaces to the <see cref="FunctionBase"/> class.
    /// </summary>
    public interface IFunction : IDisposable
    {
        /// <summary>
        /// Gets or sets the <see cref="ILogger"/> instance.
        /// </summary>
        ILogger Log { get; set; }

        /// <summary>
        /// Gets or sets the service locator for the function.
        /// </summary>
        IServiceLocator ServiceLocator { get; set; }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        Task<HttpResponseMessage> InvokeAsync(HttpRequestMessage req);
    }
}