using System;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Blob;

using Yarm.Extensions;

namespace Yarm.Wrappers
{
    /// <summary>
    /// This represents the wrapper entity for the <see cref="CloudBlockBlob"/> class.
    /// </summary>
    public class CloudBlockBlobWrapper : ICloudBlockBlobWrapper
    {
        private CloudBlockBlob _blob;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudBlockBlobWrapper"/> class.
        /// </summary>
        /// <param name="blob"><see cref="CloudBlockBlob"/> instance.</param>
        public CloudBlockBlobWrapper(CloudBlockBlob blob)
        {
            this._blob = blob.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Downloads the blob's contents as a string.
        /// </summary>
        /// <returns>Returns the string value downloaded from the blob.</returns>
        public async Task<string> DownloadTextAsync()
        {
            var result = await this._blob.DownloadTextAsync().ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value indicating whether the instance is being disposed or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this._disposed)
            {
                return;
            }

            this._blob = null;
            this._disposed = true;
        }
    }
}