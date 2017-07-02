using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;

using Yarm.Extensions;
using Yarm.Functions.FunctionFactories;
using Yarm.Services;

namespace Yarm.Functions
{
    /// <summary>
    /// This represents the function entity for the trigger.
    /// </summary>
    public class GetArmTemplateDirectoriesTimerFunction : FunctionBase, IGetArmTemplateDirectoriesTimerFunction
    {
        private readonly IGitHubService _gitHubService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArmTemplateDirectoriesTimerFunction"/> class.
        /// </summary>
        /// <param name="gitHubService"><see cref="IGitHubService"/> instance.</param>
        public GetArmTemplateDirectoriesTimerFunction(IGitHubService gitHubService)
        {
            this._gitHubService = gitHubService.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <param name="info"><see cref="TimerInfo"/> instance.</param>
        /// <returns>Returns the <see cref="Task"/>.</returns>
        public override async Task InvokeAsync(TimerInfo info)
        {
            var directories = await this._gitHubService.GetArmTemplateDirectoriesAsync().ConfigureAwait(false);
        }
    }
}
