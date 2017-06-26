using System;
using System.Net.Http;

using WorldDomination.Net.Http;

namespace Yarm.Services.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="GitHubServiceTests"/> class.
    /// </summary>
    public class GitHubServiceFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Creates a new instance of the <see cref="IGitHubService"/> class.
        /// </summary>
        /// <param name="res"><see cref="HttpResponseMessage"/> instance.</param>
        /// <returns>Returns the <see cref="IGitHubService"/> instance.</returns>
        public IGitHubService CreateInstance(HttpResponseMessage res)
        {
            var messageOptions = new HttpMessageOptions() { HttpResponseMessage = res };
            var options = new[] { messageOptions };
            var handler = new FakeHttpMessageHandler(options);

            var client = new HttpClient(handler);

            var service = new GitHubService(client);

            return service;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}
