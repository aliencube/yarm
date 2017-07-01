using System.Net;
using System.Net.Http;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

using Yarm.FunctionApp.Tests.Fixtures;
using Yarm.Functions;

namespace Yarm.FunctionApp.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="ConvertJsonToYamlHttpTrigger"/> class.
    /// </summary>
    public class ConvertJsonToYamlHttpTriggerTests : FunctionTriggerTests, IClassFixture<FunctionTriggerFixture>
    {
        private readonly FunctionTriggerFixture _fixture;
        private readonly Mock<ILogger> _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertJsonToYamlHttpTriggerTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="FunctionTriggerFixture"/> instance.</param>
        public ConvertJsonToYamlHttpTriggerTests(FunctionTriggerFixture fixture)
        {
            this._fixture = fixture;
            this._log = fixture.Log;
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> value.</param>
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        public async void Given_Instance_InvokeAsync_ShouldReturn_Response(HttpStatusCode statusCode)
        {
            this.Req = new HttpRequestMessage();
            this.Res = new HttpResponseMessage(statusCode);

            var factory = this._fixture.GetFunctionFactory(out Mock<IConvertJsonToYamlFunction> function);

            function.Setup(p => p.InvokeAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(this.Res);

            ConvertJsonToYamlHttpTrigger.FunctionFactory = factory.Object;

            var result = await ConvertJsonToYamlHttpTrigger.Run(this.Req, this._log.Object).ConfigureAwait(false);

            result.StatusCode.Should().Be(statusCode);
        }
    }
}
