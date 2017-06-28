using System;

using Autofac;

namespace Yarm.Extensions.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="ContainerBuilderExtensionsTests"/> class.
    /// </summary>
    public class ContainerBuilderExtensionsFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Arranges the fixtures.
        /// </summary>
        /// <typeparam name="TImplementer">Type of implemented instance.</typeparam>
        /// <typeparam name="TService">Type of service of instance.</typeparam>
        /// <returns>Returns the <see cref="TService"/>.</returns>
        public TService ArrangeFixturesForRegisterAsInstancePerDependency<TImplementer, TService>()
            where TImplementer : new()
        {
            Func<IComponentContext, TImplementer> func = _ => new TImplementer();
            Action<ContainerBuilder> action = p => ContainerBuilderExtensions.RegisterAsInstancePerDependency<TImplementer, TService>(p, func);

            var container = this.ArrangeContainer(action);
            var resolved = container.Resolve<TService>();

            return resolved;
        }

        /// <summary>
        /// Arranges the fixtures.
        /// </summary>
        /// <typeparam name="TImplementer">Type of implemented instance.</typeparam>
        /// <typeparam name="TService">Type of service of instance.</typeparam>
        /// <returns>Returns the <see cref="TService"/>.</returns>
        public TService ArrangeFixturesForRegisterAsSingleInstance<TImplementer, TService>()
            where TImplementer : new()
        {
            Func<IComponentContext, TImplementer> func = _ => new TImplementer();
            Action<ContainerBuilder> action = p => ContainerBuilderExtensions.RegisterAsSingleInstance<TImplementer, TService>(p, func);

            var container = this.ArrangeContainer(action);
            var resolved = container.Resolve<TService>();

            return resolved;
        }

        /// <summary>
        /// Arranges the fixtures.
        /// </summary>
        /// <typeparam name="TImplementer">Type of implemented instance.</typeparam>
        /// <typeparam name="TService">Type of service of instance.</typeparam>
        /// <returns>Returns the <see cref="TService"/>.</returns>
        public TService ArrangeFixturesForRegisterTypeAsInstancePerDependency<TImplementer, TService>()
        {
            Action<ContainerBuilder> action = p => ContainerBuilderExtensions.RegisterTypeAsInstancePerDependency<TImplementer, TService>(p);

            var container = this.ArrangeContainer(action);
            var resolved = container.Resolve<TService>();

            return resolved;
        }

        /// <summary>
        /// Arranges the fixtures.
        /// </summary>
        /// <typeparam name="TImplementer">Type of implemented instance.</typeparam>
        /// <typeparam name="TService">Type of service of instance.</typeparam>
        /// <returns>Returns the <see cref="TService"/>.</returns>
        public TService ArrangeFixturesForRegisterTypeAsSingleInstance<TImplementer, TService>()
        {
            Action<ContainerBuilder> action = p => ContainerBuilderExtensions.RegisterTypeAsSingleInstance<TImplementer, TService>(p);

            var container = this.ArrangeContainer(action);
            var resolved = container.Resolve<TService>();

            return resolved;
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

        private IContainer ArrangeContainer(Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();

            action.Invoke(builder);

            var container = builder.Build();

            return container;
        }
    }
}
