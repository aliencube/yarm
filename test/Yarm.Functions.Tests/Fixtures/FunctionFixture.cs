using System;

using Microsoft.Practices.ServiceLocation;

using Moq;

using Yarm.Services;

namespace Yarm.Functions.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for function test classes.
    /// </summary>
    public class FunctionFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Arranges the <see cref="IGetArmTemplateDirectoriesFunction"/> instance.
        /// </summary>
        /// <param name="gitHubService"><see cref="Mock{IGitHubService}"/> instance.</param>
        /// <param name="locator"><see cref="Mock{IServiceLocator}"/> instance.</param>
        /// <returns>Returns the <see cref="IGetArmTemplateDirectoriesFunction"/> instance.</returns>
        public IGetArmTemplateDirectoriesFunction ArrangeGetArmTemplateDirectoriesFunction(out Mock<IGitHubService> gitHubService, out Mock<IServiceLocator> locator)
        {
            gitHubService = new Mock<IGitHubService>();
            locator = new Mock<IServiceLocator>();

            var function = new GetArmTemplateDirectoriesFunction(gitHubService.Object);
            function.ServiceLocator = locator.Object;

            return function;
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
    }
}
