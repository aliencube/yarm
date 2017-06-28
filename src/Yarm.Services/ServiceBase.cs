using System;
using System.Net.Http;

using Yarm.Extensions;

namespace Yarm.Services
{
    /// <summary>
    /// This represents the base service entity.
    /// </summary>
    public abstract class ServiceBase : IService
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="httpClient"><see cref="System.Net.Http.HttpClient"/> instance.</param>
        protected ServiceBase(HttpClient httpClient)
        {
            this.HttpClient = httpClient.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Gets the <see cref="System.Net.Http.HttpClient"/> instance.
        /// </summary>
        protected HttpClient HttpClient { get; }

        /// <summary>
        /// Gets or sets the <see cref="HttpResponseMessage"/> instance.
        /// </summary>
        protected HttpResponseMessage Response { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value indicating whether to dispose managed resources or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                this.ReleaseManagedResources();
            }

            this.ReleaseUnmanagedResources();

            this._disposed = true;
        }

        /// <summary>
        /// Releases managed resources during the disposing event.
        /// </summary>
        protected virtual void ReleaseManagedResources()
        {
            this.Response.Dispose();
        }

        /// <summary>
        /// Releases unmanaged resources during the disposing event.
        /// </summary>
        protected virtual void ReleaseUnmanagedResources()
        {
            // Release unmanaged resources here.
        }
    }
}