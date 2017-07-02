using System;
using System.Net.Http;

using Microsoft.Azure.WebJobs;

using Yarm.Extensions;

namespace Yarm.FunctionApp.Tests
{
    /// <summary>
    /// This represents the base trigger function
    /// </summary>
    public abstract class FunctionTriggerTests : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Gets or sets the <see cref="HttpRequestMessage"/> instance.
        /// </summary>
        protected HttpRequestMessage Req { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="HttpResponseMessage"/> instance.
        /// </summary>
        protected HttpResponseMessage Res { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TimerInfo"/> instance.
        /// </summary>
        protected TimerInfo Timer { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            if (!this.Req.IsNullOrDefault())
            {
                this.Req.Dispose();
            }

            if (!this.Res.IsNullOrDefault())
            {
                this.Res.Dispose();
            }

            this._disposed = true;
        }

        /// <summary>
        /// Creates an <see cref="HttpResponseMessage"/> instance.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content"><see cref="HttpContent"/> instance.</param>
        /// <returns>Returns the <see cref="HttpRequestMessage"/> instance.</returns>
        protected HttpRequestMessage CreateRequest(string requestUri = null, HttpContent content = null)
        {
            var request = new HttpRequestMessage();
            if (!requestUri.IsNullOrWhiteSpace())
            {
                request.RequestUri = new Uri(requestUri);
            }

            if (!content.IsNullOrDefault())
            {
                request.Content = content;
            }

            return request;
        }
    }
}