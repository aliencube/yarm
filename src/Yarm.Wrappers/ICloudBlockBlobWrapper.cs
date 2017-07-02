using System;
using System.Threading.Tasks;

namespace Yarm.Wrappers
{
    /// <summary>
    /// This provides interfaces to the <see cref="CloudBlockBlobWrapper"/> class.
    /// </summary>
    public interface ICloudBlockBlobWrapper : IDisposable
    {
        /// <summary>
        /// Downloads the blob's contents as a string.
        /// </summary>
        /// <returns>Returns the string value downloaded from the blob.</returns>
        Task<string> DownloadTextAsync();
    }
}