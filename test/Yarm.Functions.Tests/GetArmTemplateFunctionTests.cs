using System;
using System.Net;
using System.Net.Http;

using FluentAssertions;

using Moq;

using Xunit;

using Yarm.Functions.Tests.Fixtures;
using Yarm.Models.Enums;
using Yarm.Models.Functions.Responses;
using Yarm.Models.GitHub;
using Yarm.Services;

namespace Yarm.Functions.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="GetArmTemplateFunction"/> class.
    /// </summary>
    public class GetArmTemplateFunctionTests : FunctionTests, IClassFixture<FunctionFixture>
    {
        private readonly FunctionFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArmTemplateFunctionTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="FunctionFixture"/> instance.</param>
        public GetArmTemplateFunctionTests(FunctionFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => new GetArmTemplateFunction(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public async void Given_NoTemplateName_InvokeAsync_ShouldReturn_NotFoundResponse()
        {
            this.Req = this.CreateRequest("http://localhost");

            var function = this._fixture.ArrangeGetArmTemplateFunction(out Mock<IGitHubService> gitHubService);

            this.Res = await function.InvokeAsync(this.Req).ConfigureAwait(false);
            this.Res.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var result = await this.Res.Content.ReadAsAsync<ErrorResponseModel>().ConfigureAwait(false);
            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            result.Message.Should().Be(ResponseMessages.TemplateNotFound);
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="name">Content name.</param>
        /// <param name="contentType">Content type.</param>
        /// <param name="templateName">Template name.</param>
        /// <param name="downloadUrl">Download URL.</param>
        /// <param name="templateContent">Template content.</param>
        [Theory]
        [InlineData("hello", "dir", "ll", "abc", "{ \"abc\": \"def\" }")]
        public async void Given_TemplateName_InvokeAsync_ShouldReturn_Result(string name, string contentType, string templateName, string downloadUrl, string templateContent)
        {
            var model = new ContentModel() { Name = name, ContentType = ContentType.Parse(contentType), DownloadUrl = downloadUrl };

            this.Req = this.CreateRequest($"http://localhost");

            var function = this._fixture.ArrangeGetArmTemplateFunction(out Mock<IGitHubService> gitHubService);
            function.TemplateName = templateName;

            gitHubService.Setup(p => p.GetArmTemplateAsync(It.IsAny<string>())).ReturnsAsync(model);
            gitHubService.Setup(p => p.GetArmTemplateAsJsonAsync(It.IsAny<string>())).ReturnsAsync(templateContent);

            this.Res = await function.InvokeAsync(this.Req).ConfigureAwait(false);
            this.Res.StatusCode.Should().Be(HttpStatusCode.OK);
            this.Res.Content.Headers.ContentType.ToString().Should().BeEquivalentTo("text/plain");

            var result = await this.Res.Content.ReadAsStringAsync().ConfigureAwait(false);
            result.Should().BeEquivalentTo(templateContent);
        }
    }
}
