using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;

using Yarm.Extensions;
using Yarm.Functions.Extensions;
using Yarm.Models.Functions.Responses;

namespace Yarm.Functions.FunctionFactories
{
    /// <summary>
    /// This represents the base function entity.
    /// </summary>
    public abstract class FunctionBase : IFunction
    {
        private static List<string> validContentTypes = new[] { "application/json" }.ToList();

        private bool _disposed;

        /// <summary>
        /// Gets or sets the <see cref="ILogger"/> instance.
        /// </summary>
        public ILogger Log { get; set; }

        /// <summary>
        /// Gets or sets the service locator for the function.
        /// </summary>
        public IServiceLocator ServiceLocator { get; set; }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public abstract Task<HttpResponseMessage> InvokeAsync(HttpRequestMessage req);

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
            // Release managed resources here.
        }

        /// <summary>
        /// Releases unmanaged resources during the disposing event.
        /// </summary>
        protected virtual void ReleaseUnmanagedResources()
        {
            // Release unmanaged resources here.
        }

        /// <summary>
        /// Checks whether the request contains payload or not.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <returns>Returns <c>True</c>, if the request contains payload; otherwise returns <c>False</c>.</returns>
        protected async Task<bool> ContainsPayloadAsync(HttpRequestMessage req)
        {
            var content = await req.Content.ReadAsStringAsync().ConfigureAwait(false);

            return !content.IsNullOrWhiteSpace();
        }

        /// <summary>
        /// Checks whether the content type has valid content type or not.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <returns>Returns <c>True</c>, if the content type has valid content type; otherwise returns <c>False</c>.</returns>
        protected bool HasValidContentType(HttpRequestMessage req)
        {
            var contentType = req.Content.Headers.ContentType.MediaType;

            return validContentTypes.ContainsEquivalent(contentType);
        }

        /// <summary>
        /// Writes an informational log message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        protected void LogInformation(string message)
        {
            this.Log.Info(message);
        }

        /// <summary>
        /// Writes a warning log message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        protected void LogWarning(string message)
        {
            this.Log.Warning(message);
        }

        /// <summary>
        /// Writes an error log message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        protected void LogError(string message)
        {
            this.Log.Error(message);
        }

        /// <summary>
        /// Creates a 200 OK response for a request.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="req">The request.</param>
        /// <param name="value">The response content.</param>
        /// <param name="mediaType">Media type.</param>
        /// <returns>The 200 OK response.</returns>
        protected HttpResponseMessage CreateOkResponse<T>(HttpRequestMessage req, T value, string mediaType = null)
        {
            return this.CreateResponse(req, HttpStatusCode.OK, value, mediaType);
        }

        /// <summary>
        /// Creates a 400 Bad Request response for a request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="message">The response content.</param>
        /// <returns>The 400 Bad Request response.</returns>
        protected HttpResponseMessage CreateBadRequestResponse(HttpRequestMessage req, string message)
        {
            var errorResponse = new ErrorResponseModel
                                    {
                                        StatusCode = (int)HttpStatusCode.BadRequest,
                                        Message = message
                                    };

            return this.CreateBadRequestResponse(req, errorResponse);
        }

        /// <summary>
        /// Creates a 400 Bad Request response for a request.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="req">The request.</param>
        /// <param name="value">The response content.</param>
        /// <returns>The 400 Bad Request response.</returns>
        protected HttpResponseMessage CreateBadRequestResponse<T>(HttpRequestMessage req, T value)
        {
            return this.CreateResponse(req, HttpStatusCode.BadRequest, value);
        }

        /// <summary>
        /// Creates a 404 Not Found response for a request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="message">The response content.</param>
        /// <returns>The 404 Not Found response.</returns>
        protected HttpResponseMessage CreateNotFoundResponse(HttpRequestMessage req, string message)
        {
            var errorResponse = new ErrorResponseModel
                                    {
                                        StatusCode = (int)HttpStatusCode.NotFound,
                                        Message = message
                                    };

            return this.CreateNotFoundResponse(req, errorResponse);
        }

        /// <summary>
        /// Creates a 404 Not Found response for a request.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="req">The request.</param>
        /// <param name="value">The response content.</param>
        /// <returns>The 404 Not Found response.</returns>
        protected HttpResponseMessage CreateNotFoundResponse<T>(HttpRequestMessage req, T value)
        {
            return this.CreateResponse(req, HttpStatusCode.NotFound, value);
        }

        /// <summary>
        /// Creates a 415 Unsupported Media Type response for a request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="message">The response content.</param>
        /// <returns>The 415 Unsupported Media Type response.</returns>
        protected HttpResponseMessage CreateUnsupportedMediaTypeResponse(HttpRequestMessage req, string message)
        {
            var errorResponse = new ErrorResponseModel
                                    {
                                        StatusCode = (int)HttpStatusCode.UnsupportedMediaType,
                                        Message = message
                                    };

            return this.CreateNotFoundResponse(req, errorResponse);
        }

        /// <summary>
        /// Creates a 415 Unsupported Media Type response for a request.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="req">The request.</param>
        /// <param name="value">The response content.</param>
        /// <returns>The 415 Unsupported Media Type response.</returns>
        protected HttpResponseMessage CreateUnsupportedMediaTypeResponse<T>(HttpRequestMessage req, T value)
        {
            return this.CreateResponse(req, HttpStatusCode.UnsupportedMediaType, value);
        }

        /// <summary>
        /// Creates a response for a request.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="req">The request.</param>
        /// <param name="statusCode">The response status code.</param>
        /// <param name="value">The response content.</param>
        /// <param name="mediaType">Media type.</param>
        /// <returns>The response.</returns>
        protected HttpResponseMessage CreateResponse<T>(HttpRequestMessage req, HttpStatusCode statusCode, T value, string mediaType = null)
        {
            if (!mediaType.IsNullOrWhiteSpace())
            {
                return req.CreateResponse(statusCode, value, mediaType);
            }

            var formatter = this.ServiceLocator.GetInstance<MediaTypeFormatter>();

            return req.CreateResponse(statusCode, value, formatter);
        }
    }
}