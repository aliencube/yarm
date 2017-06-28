using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Yarm.Extensions;
using Yarm.Functions.FunctionFactories;
using Yarm.Services;

namespace Yarm.Functions
{
    /// <summary>
    /// This represents the function entity for the trigger.
    /// </summary>
    public class GetArmTemplateDirectoriesFunction : FunctionBase, IGetArmTemplateDirectoriesFunction
    {
        private readonly IGitHubService _gitHubService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArmTemplateDirectoriesFunction"/> class.
        /// </summary>
        /// <param name="gitHubService"><see cref="IGitHubService"/> instance.</param>
        public GetArmTemplateDirectoriesFunction(IGitHubService gitHubService)
        {
            this._gitHubService = gitHubService.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public override async Task<HttpResponseMessage> InvokeAsync(HttpRequestMessage req)
        {
            var query = GetQuery(req);

            var directories = await this._gitHubService.GetArmTemplateDirectoriesAsync().ConfigureAwait(false);
            if (!query.IsNullOrWhiteSpace())
            {
                directories = directories.Where(p => p.Name.ContainsEquivalent(query)).ToList();
            }

            return this.CreateOkResponse(req, directories);
        }

        private static string GetQuery(HttpRequestMessage req)
        {
            var query = req.GetQueryNameValuePairs().SingleOrDefault(p => p.Key.IsEquivalentTo("q")).Value;

            return query;
        }
    }
}
