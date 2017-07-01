using System.Net;
using System.Net.Http;
using System.Text;

using FluentAssertions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit;

using Yarm.Functions.Tests.Fixtures;
using Yarm.Models.Functions.Responses;

namespace Yarm.Functions.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="ConvertYamlToJsonFunction"/> class.
    /// </summary>
    public class ConvertYamlToJsonFunctionTests : FunctionTests, IClassFixture<FunctionFixture>
    {
        private readonly FunctionFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertYamlToJsonFunctionTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="FunctionFixture"/> instance.</param>
        public ConvertYamlToJsonFunctionTests(FunctionFixture fixture)
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

            var function = this._fixture.ArrangeConvertYamlToJsonFunction();

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

            var function = this._fixture.ArrangeConvertYamlToJsonFunction();

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
            var yaml = $"{key}: {value}";
            var content = new StringContent(yaml, Encoding.UTF8, "application/yaml");
            this.Req = this.CreateRequest("http://localhost", content);

            var function = this._fixture.ArrangeConvertYamlToJsonFunction();

            this.Res = await function.InvokeAsync(this.Req).ConfigureAwait(false);
            this.Res.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await this.Res.Content.ReadAsStringAsync().ConfigureAwait(false);
            var deserialised = JsonConvert.DeserializeObject<string>(result);
            var json = JObject.Parse(deserialised);
            var token = json.GetValue(key);

            token.Should().NotBeNull();
            token.ToString().Should().BeEquivalentTo(value);
        }
    }
}
