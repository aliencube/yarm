using System;

using Moq;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Yarm.Models.Settings;

namespace Yarm.Helpers.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="StorageAccountHelperTests"/> class.
    /// </summary>
    public class StorageAccountHelperFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageAccountHelperFixture"/> class.
        /// </summary>
        public StorageAccountHelperFixture()
        {
            var storageAccountSettings = new Mock<StorageAccountSettings>();
            storageAccountSettings.SetupGet(p => p.ConnectionString).Returns("UseDevelopmentStorage=true");

            this.Settings = new Mock<IFunctionAppSettings>();
            this.Settings.SetupGet(p => p.StorageAccount).Returns(storageAccountSettings.Object);

            this.JsonSerialiserSettings = new JsonSerializerSettings()
                                              {
                                                  ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                                  Converters = { new StringEnumConverter() },
                                                  Formatting = Formatting.Indented,
                                                  NullValueHandling = NullValueHandling.Ignore,
                                                  MissingMemberHandling = MissingMemberHandling.Ignore
                                              };

            this.Helper = new StorageAccountHelper(this.Settings.Object, this.JsonSerialiserSettings);
        }

        /// <summary>
        /// Gets the <see cref="Mock{IFunctionAppSettings}"/> instance.
        /// </summary>
        public Mock<IFunctionAppSettings> Settings { get; }

        /// <summary>
        /// Gets the <see cref="Newtonsoft.Json.JsonSerializerSettings"/> instance.
        /// </summary>
        public JsonSerializerSettings JsonSerialiserSettings { get; }

        /// <summary>
        /// Gets the <see cref="IStorageAccountHelper"/> instance.
        /// </summary>
        public IStorageAccountHelper Helper { get; }

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