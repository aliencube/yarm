using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

using Newtonsoft.Json;

using Yarm.Extensions;
using Yarm.Models.Settings;
using Yarm.Wrappers;

namespace Yarm.Helpers
{
    /// <summary>
    /// This represents the helper entity for Azure Storage Account.
    /// </summary>
    public class StorageAccountHelper : IStorageAccountHelper
    {
        private readonly IFunctionAppSettings _appSettings;
        private readonly JsonSerializerSettings _serialiserSettings;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageAccountHelper"/> class.
        /// </summary>
        /// <param name="appSettings"><see cref="IFunctionAppSettings"/> instance.</param>
        /// <param name="serialiserSettings"><see cref="JsonSerializerSettings"/> instance.</param>
        public StorageAccountHelper(IFunctionAppSettings appSettings, JsonSerializerSettings serialiserSettings)
        {
            this._appSettings = appSettings.ThrowIfNullOrDefault();
            this._serialiserSettings = serialiserSettings.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Gets the <see cref="ICloudBlockBlobWrapper"/> instance.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        /// <param name="blobName">Blob name.</param>
        /// <returns>Returns the <see cref="ICloudBlockBlobWrapper"/> instance.</returns>
        public async Task<ICloudBlockBlobWrapper> GetCloudBlockBlobAsync(string containerName, string blobName)
        {
            containerName.ThrowIfNullOrWhiteSpace();
            blobName.ThrowIfNullOrWhiteSpace();

            var blob = await GetCloudBlockBlobAsync(containerName, blobName, this._appSettings.StorageAccount.ConnectionString).ConfigureAwait(false);

            return blob == null ? null : new CloudBlockBlobWrapper(blob);
        }

        /// <summary>
        /// Downloads the blob as a given type.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="containerName">Container name.</param>
        /// <param name="blobName">Blob name.</param>
        /// <returns>Returns the blob as a given type.</returns>
        public async Task<T> DownloadBlobAsync<T>(string containerName, string blobName)
        {
            containerName.ThrowIfNullOrWhiteSpace();
            blobName.ThrowIfNullOrWhiteSpace();

            var blob = await GetCloudBlockBlobAsync(containerName, blobName, this._appSettings.StorageAccount.ConnectionString).ConfigureAwait(false);
            if (blob == null)
            {
                return default(T);
            }

            var serialised = await blob.DownloadTextAsync().ConfigureAwait(false);
            var deserialised = JsonConvert.DeserializeObject<T>(serialised, this._serialiserSettings);

            return deserialised;
        }

        /// <summary>
        /// Saves the instance into Azure blob storage.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="instance">Instance to store.</param>
        /// <param name="containerName">Container name.</param>
        /// <param name="blobName">Blob name.</param>
        /// <returns>Returns <c>True</c>, if stored successfully; otherwise throws an exception.</returns>
        public async Task<bool> SaveBlobAsync<T>(T instance, string containerName, string blobName)
        {
            instance.ThrowIfNullOrDefault();
            containerName.ThrowIfNullOrWhiteSpace();
            blobName.ThrowIfNullOrWhiteSpace();

            var serialised = JsonConvert.SerializeObject(instance, this._serialiserSettings);

            var blob = await GetCloudBlockBlobAsync(containerName, blobName, this._appSettings.StorageAccount.ConnectionString, false).ConfigureAwait(false);
            blob.Properties.ContentType = "application/json";
            await blob.UploadTextAsync(serialised).ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }

        private static async Task<CloudBlockBlob> GetCloudBlockBlobAsync(string containerName, string blobName, string connectionString, bool ensureBlobExists = true)
        {
            var sa = CloudStorageAccount.Parse(connectionString);

            var client = sa.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync().ConfigureAwait(false);

            var blob = container.GetBlockBlobReference(blobName);

            if (ensureBlobExists)
            {
                var exists = await blob.ExistsAsync().ConfigureAwait(false);

                return exists ? blob : null;
            }

            return blob;
        }
    }
}