using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

using FluentAssertions;

using Newtonsoft.Json;

using WorldDomination.Net.Http;

using Xunit;

namespace Yarm.Extensions.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="HttpClientExtensions"/> class.
    /// </summary>
    public class HttpClientExtensionsTests
    {
        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_PatchAsync_ShouldThrow_Exception()
        {
            var client = new HttpClient();
            var requestUri = "https://localhost/api";
            var value = new object();

            Func<Task> func = async () => await HttpClientExtensions.PatchAsync(null, requestUri, value).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();

            func = async () => await HttpClientExtensions.PatchAsync(client, null, value).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();

            func = async () => await HttpClientExtensions.PatchAsync(client, requestUri, (object)null).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="msg">Message value.</param>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="statusCode">Status code.</param>
        [Theory]
        [InlineData("Hello World", "http://localhost", HttpStatusCode.OK)]
        public async void Given_Parameters_PatchAsync_ShouldReturn_Result(string msg, string requestUri, HttpStatusCode statusCode)
        {
            var value = new { Message = msg };
            var content = new ObjectContent<object>(value, new JsonMediaTypeFormatter());
            var response = new HttpResponseMessage(statusCode) { Content = content };
            var messageOptions = new HttpMessageOptions() { HttpResponseMessage = response };
            var options = new[] { messageOptions };
            var handler = new FakeHttpMessageHandler(options);
            var client = new HttpClient(handler);

            var result = await HttpClientExtensions.PatchAsync(client, requestUri, value).ConfigureAwait(false);
            result.StatusCode.Should().Be(statusCode);

            var serialised = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            serialised.Should().BeEquivalentTo(JsonConvert.SerializeObject(value));
        }
    }
}
