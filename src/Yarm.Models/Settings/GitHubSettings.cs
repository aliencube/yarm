using System.Configuration;

namespace Yarm.Models.Settings
{
    /// <summary>
    /// This represents the settings entity for GitHub.
    /// </summary>
    public class GitHubSettings
    {
        private const string GitHubApiBaseUri = "GithubApiBaseUri";
        private const string GitHubRepositoryContentUri = "GitHubRepositoryContentUri";
        private const string GitHubAzureUsername = "GitHubAzureUsername";
        private const string GitHubAzureQuickstartTemplatesRepository = "GitHubAzureQuickstartTemplatesRepository";

        /// <summary>
        /// Gets the GitHub API base URI.
        /// </summary>
        public virtual string ApiBaseUri => $"{ConfigurationManager.AppSettings[GitHubApiBaseUri].TrimEnd('/')}/";

        /// <summary>
        /// Gets the GitHub repository content request URI.
        /// </summary>
        public virtual string RepositoryContentUri => ConfigurationManager.AppSettings[GitHubRepositoryContentUri].Trim('/');

        /// <summary>
        /// Gets the GitHub username for Azure.
        /// </summary>
        public virtual string AzureUsername => ConfigurationManager.AppSettings[GitHubAzureUsername];

        /// <summary>
        /// Gets the repository name for Azure Quick Start Templates.
        /// </summary>
        public virtual string AzureQuickstartTemplatesRepository => ConfigurationManager.AppSettings[GitHubAzureQuickstartTemplatesRepository];
    }
}