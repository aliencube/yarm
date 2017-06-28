using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using FluentAssertions;

using Moq;

using Xunit;

using Yarm.Functions.Tests.Fixtures;
using Yarm.Models.Enums;
using Yarm.Models.GitHub;
using Yarm.Services;

namespace Yarm.Functions.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="GetArmTemplateDirectoriesFunction"/> class.
    /// </summary>
    public class GetArmTemplateDirectoriesFunctionTests : FunctionTests, IClassFixture<FunctionFixture>
    {
        private readonly FunctionFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArmTemplateDirectoriesFunctionTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="FunctionFixture"/> instance.</param>
        public GetArmTemplateDirectoriesFunctionTests(FunctionFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => new GetArmTemplateDirectoriesFunction(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="name">Content name.</param>
        /// <param name="contentType">Content type.</param>
        [Theory]
        [InlineData("hello", "dir")]
        public async void Given_NoQueryString_InvokeAsync_ShouldReturn_Result(string name, string contentType)
        {
            var models = new List<ContentModel>()
                             {
                                 new ContentModel() { Name = name, ContentType = ContentType.Parse(contentType) }
                             };

            this.Req = this.CreateRequest("http://localhost");

            var function = this._fixture.ArrangeGetArmTemplateDirectoriesFunction(out Mock<IGitHubService> gitHubService);

            gitHubService.Setup(p => p.GetArmTemplateDirectoriesAsync()).ReturnsAsync(models);

            this.Res = await function.InvokeAsync(this.Req).ConfigureAwait(false);
            this.Res.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await this.Res.Content.ReadAsAsync<List<ContentModel>>().ConfigureAwait(false);
            result.Should().HaveCount(models.Count);
            result.Single().Name.Should().BeEquivalentTo(name);
            result.Single().ContentType.Should().BeEquivalentTo(contentType);
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="name">Content name.</param>
        /// <param name="contentType">Content type.</param>
        /// <param name="query">Querystring value.</param>
        /// <param name="countExpected">Expected result count.</param>
        [Theory]
        [InlineData("hello", "dir", "ll", 1)]
        [InlineData("hello", "dir", "oo", 0)]
        public async void Given_QueryString_InvokeAsync_ShouldReturn_Result(string name, string contentType, string query, int countExpected)
        {
            var models = new List<ContentModel>()
                             {
                                 new ContentModel() { Name = name, ContentType = ContentType.Parse(contentType) }
                             };

            this.Req = this.CreateRequest($"http://localhost?q={query}");

            var function = this._fixture.ArrangeGetArmTemplateDirectoriesFunction(out Mock<IGitHubService> gitHubService);

            gitHubService.Setup(p => p.GetArmTemplateDirectoriesAsync()).ReturnsAsync(models);

            this.Res = await function.InvokeAsync(this.Req).ConfigureAwait(false);
            this.Res.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await this.Res.Content.ReadAsAsync<List<ContentModel>>().ConfigureAwait(false);
            result.Should().HaveCount(countExpected);
        }
    }
}
