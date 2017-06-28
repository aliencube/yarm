using System.Configuration;

using Autofac;

using FluentAssertions;

using Xunit;

using Yarm.Functions.ServiceLocators;
using Yarm.Functions.Tests.Fixtures;
using Yarm.Models.Settings;

namespace Yarm.Functions.Tests.ServiceLocators
{
    /// <summary>
    /// This represents the test entity for the <see cref="AppModule"/> class.
    /// </summary>
    public class AppModuleTests : IClassFixture<AppModuleFixture>
    {
        private readonly AppModuleFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppModuleTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="AppModuleFixture"/> instance.</param>
        public AppModuleTests(AppModuleFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Test whether the module should load all dependencies or not.
        /// </summary>
        [Fact]
        public void Given_AppModule_ShouldLoad_Dependencies()
        {
            var container = this._fixture.ArrangeContainer(p => p.RegisterModule<AppModule>());

            var appSettings = container.Resolve<IFunctionAppSettings>();

            appSettings.StorageAccount.ConnectionString.Should()
                       .BeEquivalentTo(ConfigurationManager.AppSettings["AzureWebJobsStorage"]);
        }
    }
}
