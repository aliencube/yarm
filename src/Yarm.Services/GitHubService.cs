using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Yarm.Extensions;
using Yarm.Models.Enums;
using Yarm.Models.GitHub;

namespace Yarm.Services
{
    /// <summary>
    /// This represents the service entity for the GitHub REST API calls.
    /// </summary>
    public class GitHubService
    {
        private const string GitHubContentUri = "https://api.github.com/repos/{0}/{1}/contents";
        private const string AzureUsername = "Azure";
        private const string ArmTemplateRepository = "azure-quickstart-templates";
        private const string ContributionGuideDirectory = "1-CONTRIBUTION-GUIDE";

        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubService"/> class.
        /// </summary>
        /// <param name="httpClient"><see cref="HttpClient"/> instance.</param>
        public GitHubService(HttpClient httpClient)
        {
            this._httpClient = httpClient.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Gets the list of ARM template directories.
        /// </summary>
        /// <returns>Returns the list of ARM template directories.</returns>
        public async Task<List<ContentModel>> GetArmTemplateDirectoriesAsync()
        {
            var requestUri = string.Format(GitHubContentUri, AzureUsername, ArmTemplateRepository);
            using (var response = await this._httpClient.GetAsync(requestUri).ConfigureAwait(false))
            {
                var contents = await response.Content.ReadAsAsync<List<ContentModel>>().ConfigureAwait(false);

                return contents.Where(IsContentEligible).ToList();
            }
        }

        private static bool IsContentEligible(ContentModel model)
        {
            return model.ContentType.Equals(ContentType.Directory) && !model.Name.IsEquivalentTo(ContributionGuideDirectory);
        }
    }
}
