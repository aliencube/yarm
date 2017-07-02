using System;
using System.Threading.Tasks;

using Yarm.Wrappers;

namespace Yarm.Helpers
{
    /// <summary>
    /// This provides interfaces to the <see cref="StorageAccountHelper"/> class.
    /// </summary>
    public interface IStorageAccountHelper : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="ICloudBlockBlobWrapper"/> instance.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        /// <param name="blobName">Blob name.</param>
        /// <returns>Returns the <see cref="ICloudBlockBlobWrapper"/> instance.</returns>
        Task<ICloudBlockBlobWrapper> GetCloudBlockBlobAsync(string containerName, string blobName);

        /// <summary>
        /// Downloads the blob as a given type.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="containerName">Container name.</param>
        /// <param name="blobName">Blob name.</param>
        /// <returns>Returns the blob as a given type.</returns>
        Task<T> DownloadBlobAsync<T>(string containerName, string blobName);

        /// <summary>
        /// Saves the instance into Azure blob storage.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="instance">Instance to store.</param>
        /// <param name="containerName">Container name.</param>
        /// <param name="blobName">Blob name.</param>
        /// <returns>Returns <c>True</c>, if stored successfully; otherwise throws an exception.</returns>
        Task<bool> SaveBlobAsync<T>(T instance, string containerName, string blobName);
    }
}