using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

using Yarm.Services.Tests.Fixtures;

namespace Yarm.Services.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="GitHubService"/> class.
    /// </summary>
    public class GitHubServiceTests : IClassFixture<GitHubServiceFixture>
    {
        private readonly HttpResponseMessage _res;
        private readonly IGitHubService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubServiceTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="GitHubServiceFixture"/> instance.</param>
        public GitHubServiceTests(GitHubServiceFixture fixture)
        {
            this._res = fixture.Response;
            this._service = fixture.GitHubService;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => new GitHubService(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> value.</param>
        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        public void Given_ErrorResponse_GetArmTemplateDirectoriesAsync_ShouldThrow_Exception(HttpStatusCode statusCode)
        {
            this._res.StatusCode = statusCode;

            Func<Task> func = async () => await this._service.GetArmTemplateDirectoriesAsync().ConfigureAwait(false);
            func.ShouldThrow<HttpRequestException>();
        }
    }
}
