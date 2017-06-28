namespace Yarm.Models.Settings
{
    /// <summary>
    /// This represents the settings entity for Azure Functions app.
    /// </summary>
    public class FunctionAppSettings : IFunctionAppSettings
    {
        private bool _disposed;

        /// <summary>
        /// Gets the <see cref="StorageAccountSettings"/> instance.
        /// </summary>
        public virtual StorageAccountSettings StorageAccount => new StorageAccountSettings();

        /// <summary>
        /// Gets the <see cref="GitHubSettings"/> instance.
        /// </summary>
        public virtual GitHubSettings GitHub => new GitHubSettings();

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
    }
}