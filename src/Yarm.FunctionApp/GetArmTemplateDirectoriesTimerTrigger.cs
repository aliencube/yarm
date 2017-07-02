using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using Yarm.Functions;
using Yarm.Functions.FunctionFactories;

namespace Yarm.FunctionApp
{
    /// <summary>
    /// This represents the timer trigger entity for the GetArmTemplateDirectories event.
    /// </summary>
    public static class GetArmTemplateDirectoriesTimerTrigger
    {
        /// <summary>
        /// Gets or sets the <see cref="Functions.FunctionFactories.FunctionFactory"/> instance.
        /// </summary>
        public static FunctionFactory FunctionFactory { get; set; } = new FunctionFactory();

        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="timer"><see cref="TimerInfo"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="Task"/>.</returns>
        public static async Task Run(TimerInfo timer, ILogger log)
        {
            await FunctionFactory.Create<IGetArmTemplateDirectoriesTimerFunction>(log)
                                 .InvokeAsync(timer)
                                 .ConfigureAwait(false);
        }
    }
}