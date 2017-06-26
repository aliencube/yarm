using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

using Yarm.Models.Enums;
using Yarm.Models.GitHub;
using Yarm.Services.Tests.Fixtures;

namespace Yarm.Services.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="GitHubService"/> class.
    /// </summary>
    public class GitHubServiceTests : IClassFixture<GitHubServiceFixture>
    {
        private readonly GitHubServiceFixture _fixture;

        private IGitHubService _service;
        private HttpResponseMessage _res;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubServiceTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="GitHubServiceFixture"/> instance.</param>
        public GitHubServiceTests(GitHubServiceFixture fixture)
        {
            this._fixture = fixture;
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
            using (this._res = new HttpResponseMessage(statusCode))
            {
                this._service = this._fixture.CreateInstance(this._res);

                Func<Task> func = async () => await this._service.GetArmTemplateDirectoriesAsync().ConfigureAwait(false);
                func.ShouldThrow<HttpRequestException>();
            }
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> value.</param>
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        public async void Given_Response_GetArmTemplateDirectoriesAsync_ShouldReturn_Result(HttpStatusCode statusCode)
        {
            var models = new[]
                             {
                                 new ContentModel() { ContentType = ContentType.File, Name = "abc" },
                                 new ContentModel() { ContentType = ContentType.Directory, Name = "pqr" },
                                 new ContentModel() { ContentType = ContentType.Directory, Name = "1-CONTRIBUTION-GUIDE" }
                             }.ToList();

            using (var content = new StringContent(JsonConvert.SerializeObject(models), Encoding.UTF8, "application/json"))
            using (this._res = new HttpResponseMessage(statusCode) { Content = content })
            {
                this._service = this._fixture.CreateInstance(this._res);
                var result = await this._service.GetArmTemplateDirectoriesAsync().ConfigureAwait(false);

                result.Should().HaveCount(1);
                result.Single().Name.Should().BeEquivalentTo("pqr");
            }
        }
    }
}
