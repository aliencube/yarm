using System;

namespace Yarm.Models.Settings
{
    /// <summary>
    /// This provides interfaces to the <see cref="FunctionAppSettings"/> class.
    /// </summary>
    public interface IFunctionAppSettings : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="StorageAccountSettings"/> instance.
        /// </summary>
        StorageAccountSettings StorageAccount { get; }
    }
}