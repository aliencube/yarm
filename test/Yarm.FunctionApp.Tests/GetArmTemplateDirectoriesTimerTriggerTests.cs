using System.Net;
using System.Net.Http;

using FluentAssertions;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Timers;
using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

using Yarm.FunctionApp.Tests.Fixtures;
using Yarm.Functions;

namespace Yarm.FunctionApp.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="GetArmTemplateDirectoriesTimerTrigger"/> class.
    /// </summary>
    public class GetArmTemplateDirectoriesTimerTriggerTests : FunctionTriggerTests, IClassFixture<FunctionTriggerFixture>
    {
        private readonly FunctionTriggerFixture _fixture;
        private readonly Mock<ILogger> _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArmTemplateDirectoriesTimerTriggerTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="FunctionTriggerFixture"/> instance.</param>
        public GetArmTemplateDirectoriesTimerTriggerTests(FunctionTriggerFixture fixture)
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
            this.Timer = new TimerInfo(new Mock<TimerSchedule>().Object, new ScheduleStatus());

            var factory = this._fixture.GetFunctionFactory(out Mock<IGetArmTemplateDirectoriesTimerFunction> function);

            GetArmTemplateDirectoriesTimerTrigger.FunctionFactory = factory.Object;

            await GetArmTemplateDirectoriesTimerTrigger.Run(this.Timer, this._log.Object).ConfigureAwait(false);
        }
    }
}
