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
        /// Initializes a new instance of the <see cref="GitHubServiceFixture"/> class.
        /// </summary>
        public GitHubServiceFixture()
        {
            this.Response = new HttpResponseMessage();

            var messageOptions = new HttpMessageOptions() { HttpResponseMessage = this.Response };
            var options = new[] { messageOptions };
            var handler = new FakeHttpMessageHandler(options);

            var client = new HttpClient(handler);

            this.GitHubService = new GitHubService(client);
        }

        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/> instance.
        /// </summary>
        public HttpResponseMessage Response { get; }

        /// <summary>
        /// Gets the <see cref="IGitHubService"/> instance.
        /// </summary>
        public IGitHubService GitHubService { get; }

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
