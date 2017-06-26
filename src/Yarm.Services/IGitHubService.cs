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
    }
}