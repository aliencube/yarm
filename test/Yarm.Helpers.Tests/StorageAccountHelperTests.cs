using System;
using System.Threading.Tasks;

using FluentAssertions;

using Moq;

using Newtonsoft.Json;

using Xunit;

using Yarm.Helpers.Tests.Fixtures;
using Yarm.Models.Settings;

namespace Yarm.Helpers.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="StorageAccountHelper"/> class.
    /// </summary>
    public class StorageAccountHelperTests : IClassFixture<StorageAccountHelperFixture>
    {
        private readonly Mock<IFunctionAppSettings> _settings;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IStorageAccountHelper _helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageAccountHelperTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="StorageAccountHelperFixture"/> instance.</param>
        public StorageAccountHelperTests(StorageAccountHelperFixture fixture)
        {
            this._settings = fixture.Settings;
            this._serializerSettings = fixture.JsonSerialiserSettings;
            this._helper = fixture.Helper;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => new StorageAccountHelper(null, this._serializerSettings);
            action.ShouldThrow<ArgumentNullException>();

            action = () => new StorageAccountHelper(this._settings.Object, null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        /// <param name="blobName">Blob name.</param>
        [Theory]
        [InlineData("hello", "world")]
        public void Given_NullParameter_GetCloudBlockBlobAsync_ShouldThrow_Exception(string containerName, string blobName)
        {
            Func<Task> func = async () => await this._helper.GetCloudBlockBlobAsync(null, blobName).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();

            func = async () => await this._helper.GetCloudBlockBlobAsync(containerName, null).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        /// <param name="blobName">Blob name.</param>
        [Theory]
        [InlineData("hello", "world")]
        public void Given_NullParameter_DownloadBlobAsync_ShouldThrow_Exception(string containerName, string blobName)
        {
            Func<Task> func = async () => await this._helper.DownloadBlobAsync<dynamic>(null, blobName).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();

            func = async () => await this._helper.DownloadBlobAsync<dynamic>(containerName, null).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        /// <param name="blobName">Blob name.</param>
        [Theory]
        [InlineData("hello", "world")]
        public void Given_NullParameter_SaveBlobAsync_ShouldThrow_Exception(string containerName, string blobName)
        {
            Func<Task> func = async () => await this._helper.SaveBlobAsync<object>(null, containerName, blobName).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();

            func = async () => await this._helper.SaveBlobAsync<object>(new object(), null, blobName).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();

            func = async () => await this._helper.SaveBlobAsync<object>(new object(), containerName, null).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }
    }
}