using System.Configuration;

namespace Yarm.Models.Settings
{
    /// <summary>
    /// This represents the settings entity for Azure Storage Account.
    /// </summary>
    public class StorageAccountSettings
    {
        private const string StorageAccountConnectionString = "AzureWebJobsStorage";

        /// <summary>
        /// Gets the connection string for Azure Storage Account.
        /// </summary>
        public virtual string ConnectionString => ConfigurationManager.AppSettings[StorageAccountConnectionString];
    }
}