using System;
using System.Net.Http;

using Moq;

using WorldDomination.Net.Http;

using Yarm.Models.Settings;

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
            var github = new Mock<GitHubSettings>();
            github.SetupGet(p => p.ApiBaseUri).Returns("http://localhost");
            github.SetupGet(p => p.RepositoryContentUri).Returns("repos/{0}/{1}/contents");
            github.SetupGet(p => p.AzureUsername).Returns("arm");
            github.SetupGet(p => p.AzureQuickstartTemplatesRepository).Returns("templates");

            var settings = new Mock<IFunctionAppSettings>();
            settings.SetupGet(p => p.GitHub).Returns(github.Object);

            var messageOptions = new HttpMessageOptions() { HttpResponseMessage = res };
            var options = new[] { messageOptions };
            var handler = new FakeHttpMessageHandler(options);

            var client = new HttpClient(handler);

            var service = new GitHubService(settings.Object, client);

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
