using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Yarm.Models.GitHub;

namespace Yarm.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="GitHubService"/> class.
    /// </summary>
    public interface IGitHubService : IDisposable
    {
        /// <summary>
        /// Gets the list of ARM template directories.
        /// </summary>
        /// <returns>Returns the list of ARM template directories.</returns>
        Task<List<ContentModel>> GetArmTemplateDirectoriesAsync();

        /// <summary>
        /// Gets the ARM template.
        /// </summary>
        /// <param name="directoryName">GitHub directory name.</param>
        /// <returns>Returns the ARM template.</returns>
        Task<ContentModel> GetArmTemplateAsync(string directoryName);

        /// <summary>
        /// Gets the ARM template as JSON format.
        /// </summary>
        /// <param name="downloadUrl">GitHub download URL.</param>
        /// <returns>Returns the ARM template as JSON format.</returns>
        Task<string> GetArmTemplateAsJsonAsync(string downloadUrl);
    }
}