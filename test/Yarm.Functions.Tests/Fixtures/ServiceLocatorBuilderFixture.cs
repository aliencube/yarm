using System;

using Autofac;

using Microsoft.Practices.ServiceLocation;

using Yarm.Functions.ServiceLocators;
using Yarm.Functions.Tests.ServiceLocators;

namespace Yarm.Functions.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="ServiceLocatorBuilderTests"/> class.
    /// </summary>
    public class ServiceLocatorBuilderFixture : IDisposable
    {
        private IServiceLocator _locator;
        private bool _disposed;

        /// <summary>
        /// Arranges the <see cref="IServiceLocator"/> instance.
        /// </summary>
        /// <param name="action">Action for <see cref="ContainerBuilder"/> instance.</param>
        /// <returns>Returns the <see cref="IServiceLocator"/> instance.</returns>
        public IServiceLocator ArrangeLocator(Action<IServiceLocatorBuilder> action)
        {
            var builder = new ServiceLocatorBuilder();

            action.Invoke(builder);

            this._locator = builder.Build();

            return this._locator;
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

            this._locator = null;

            this._disposed = true;
        }
    }
}
