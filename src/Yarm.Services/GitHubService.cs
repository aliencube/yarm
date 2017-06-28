using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Yarm.Extensions;
using Yarm.Models.Enums;
using Yarm.Models.GitHub;
using Yarm.Models.Settings;

namespace Yarm.Services
{
    /// <summary>
    /// This represents the service entity for the GitHub REST API calls.
    /// </summary>
    public class GitHubService : ServiceBase, IGitHubService
    {
        private const string ArmTemplateName = "azuredeploy.json";

        private static MediaTypeWithQualityHeaderValue acceptHeader = new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json");
        private static ProductInfoHeaderValue userAgentHeader = new ProductInfoHeaderValue("Mozilla", "5.0");
        private static List<string> directoriesToExclude = new[] { ".github", "1-CONTRIBUTION-GUIDE" }.ToList();

        private readonly IFunctionAppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubService"/> class.
        /// </summary>
        /// <param name="appSettings"><see cref="IFunctionAppSettings"/> instance.</param>
        /// <param name="httpClient"><see cref="HttpClient"/> instance.</param>
        public GitHubService(IFunctionAppSettings appSettings, HttpClient httpClient)
            : base(httpClient)
        {
            this._appSettings = appSettings.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Gets the list of ARM template directories.
        /// </summary>
        /// <returns>Returns the list of ARM template directories.</returns>
        public async Task<List<ContentModel>> GetArmTemplateDirectoriesAsync()
        {
            this.AddRequestHeaders();

            var github = this._appSettings.GitHub;
            var requestUri = $"{github.ApiBaseUri}{string.Format(github.RepositoryContentUri, github.AzureUsername, github.AzureQuickstartTemplatesRepository)}";
            this.Response = await this.HttpClient.GetAsync(requestUri).ConfigureAwait(false);
            this.Response.EnsureSuccessStatusCode();

            var contents = await this.Response.Content.ReadAsAsync<List<ContentModel>>().ConfigureAwait(false);

            this.RemoveRequestHeaders();

            return contents.Where(IsContentEligible).ToList();
        }

        /// <summary>
        /// Gets the ARM template.
        /// </summary>
        /// <param name="directoryName">GitHub directory name.</param>
        /// <returns>Returns the ARM template.</returns>
        public async Task<ContentModel> GetArmTemplateAsync(string directoryName)
        {
            this.AddRequestHeaders();

            var github = this._appSettings.GitHub;
            var requestUri = $"{github.ApiBaseUri}{string.Format(github.RepositoryContentUri, github.AzureUsername, github.AzureQuickstartTemplatesRepository)}/{directoryName}";
            this.Response = await this.HttpClient.GetAsync(requestUri).ConfigureAwait(false);
            this.Response.EnsureSuccessStatusCode();

            var contents = await this.Response.Content.ReadAsAsync<List<ContentModel>>().ConfigureAwait(false);

            this.RemoveRequestHeaders();

            return contents.SingleOrDefault(p => p.Name.IsEquivalentTo(ArmTemplateName));
        }

        /// <summary>
        /// Gets the ARM template as JSON format.
        /// </summary>
        /// <param name="downloadUrl">GitHub download URL.</param>
        /// <returns>Returns the ARM template as JSON format.</returns>
        public async Task<string> GetArmTemplateAsJsonAsync(string downloadUrl)
        {
            this.AddRequestHeaders();

            this.Response = await this.HttpClient.GetAsync(downloadUrl).ConfigureAwait(false);
            this.Response.EnsureSuccessStatusCode();

            var template = await this.Response.Content.ReadAsStringAsync().ConfigureAwait(false);

            this.RemoveRequestHeaders();

            return template;
        }

        private void AddRequestHeaders()
        {
            // https://gist.github.com/BellaCode/c0ba0a842bbe22c9215e
            this.HttpClient.DefaultRequestHeaders.Accept.Add(acceptHeader);
            this.HttpClient.DefaultRequestHeaders.UserAgent.Add(userAgentHeader);
        }

        private void RemoveRequestHeaders()
        {
            this.HttpClient.DefaultRequestHeaders.Accept.Remove(acceptHeader);
            this.HttpClient.DefaultRequestHeaders.UserAgent.Remove(userAgentHeader);
        }

        private static bool IsContentEligible(ContentModel model)
        {
            return model.ContentType.Equals(ContentType.Directory) && !directoriesToExclude.ContainsEquivalent(model.Name);
        }
    }
}
