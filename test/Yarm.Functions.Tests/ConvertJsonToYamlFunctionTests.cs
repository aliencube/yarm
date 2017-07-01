using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

using Yarm.Functions.Tests.Fixtures;
using Yarm.Models.Functions.Responses;

namespace Yarm.Functions.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="ConvertJsonToYamlFunction"/> class.
    /// </summary>
    public class ConvertJsonToYamlFunctionTests : FunctionTests, IClassFixture<FunctionFixture>
    {
        private readonly FunctionFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertJsonToYamlFunctionTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="FunctionFixture"/> instance.</param>
        public ConvertJsonToYamlFunctionTests(FunctionFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the method should return an error response or not.
        /// </summary>
        [Fact]
        public async void Given_NoPayload_InvokeAsync_ShouldReturn_BadRequestResponse()
        {
            var content = new StringContent(string.Empty);
            this.Req = this.CreateRequest("http://localhost", content);

            var function = this._fixture.ArrangeConvertJsonToYamlFunction();

            this.Res = await function.InvokeAsync(this.Req).ConfigureAwait(false);
            this.Res.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await this.Res.Content.ReadAsAsync<ErrorResponseModel>().ConfigureAwait(false);
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            result.Message.Should().Be(ResponseMessages.RequestPayloadNotFound);
        }

        /// <summary>
        /// Tests whether the method should return an error response or not.
        /// </summary>
        [Fact]
        public async void Given_InvalidContentType_InvokeAsync_ShouldReturn_BadRequestResponse()
        {
            var yaml = "foo: bar";
            var content = new StringContent(yaml, Encoding.UTF8, "application/xml");
            this.Req = this.CreateRequest("http://localhost", content);

            var function = this._fixture.ArrangeConvertJsonToYamlFunction();

            this.Res = await function.InvokeAsync(this.Req).ConfigureAwait(false);
            this.Res.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await this.Res.Content.ReadAsAsync<ErrorResponseModel>().ConfigureAwait(false);
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            result.Message.Should().Be(ResponseMessages.InvalidRequestPayloadContentType);
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        [Theory]
        [InlineData("foo", "bar")]
        public async void Given_Payload_InvokeAsync_ShouldReturn_Result(string key, string value)
        {
            var dic = new Dictionary<string, string>() { { key, value } };
            var json = JsonConvert.SerializeObject(dic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            this.Req = this.CreateRequest("http://localhost", content);

            var function = this._fixture.ArrangeConvertJsonToYamlFunction();

            this.Res = await function.InvokeAsync(this.Req).ConfigureAwait(false);
            this.Res.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await this.Res.Content.ReadAsStringAsync().ConfigureAwait(false);

            result.Should().StartWithEquivalent(key);
            result.Should().ContainEquivalentOf($"{key}: {value}");
        }
    }
}
