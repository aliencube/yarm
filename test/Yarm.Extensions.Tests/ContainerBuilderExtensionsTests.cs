using System;

using Autofac;

using FluentAssertions;

using Xunit;

using Yarm.Extensions.Tests.Fixtures;

namespace Yarm.Extensions.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="ContainerBuilderExtensions"/> class.
    /// </summary>
    public class ContainerBuilderExtensionsTests : IClassFixture<ContainerBuilderExtensionsFixture>
    {
        private readonly ContainerBuilderExtensionsFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerBuilderExtensionsTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="ContainerBuilderExtensionsFixture"/> instance.</param>
        public ContainerBuilderExtensionsTests(ContainerBuilderExtensionsFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_RegisterAsInstancePerDependency_ShouldThrow_Exception()
        {
            Action action = () => ContainerBuilderExtensions.RegisterAsInstancePerDependency<Foo, IFoo>(null, null);
            action.ShouldThrow<ArgumentNullException>();

            action = () => ContainerBuilderExtensions.RegisterAsInstancePerDependency<Foo, IFoo>(new ContainerBuilder(), null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should resolve instance or not.
        /// </summary>
        [Fact]
        public void Given_Instance_RegisterAsInstancePerDependency_ShouldReturn_Result()
        {
            var resolved = this._fixture.ArrangeFixturesForRegisterAsInstancePerDependency<Foo, IFoo>();

            resolved.Should().BeOfType<Foo>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_RegisterAsSingleInstance_ShouldThrow_Exception()
        {
            Action action = () => ContainerBuilderExtensions.RegisterAsSingleInstance<Foo, IFoo>(null, null);
            action.ShouldThrow<ArgumentNullException>();

            action = () => ContainerBuilderExtensions.RegisterAsSingleInstance<Foo, IFoo>(new ContainerBuilder(), null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should resolve instance or not.
        /// </summary>
        [Fact]
        public void Given_Instance_RegisterAsSingleInstance_ShouldReturn_Result()
        {
            var resolved = this._fixture.ArrangeFixturesForRegisterAsSingleInstance<Foo, IFoo>();

            resolved.Should().BeOfType<Foo>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_RegisterTypeAsInstancePerDependency_ShouldThrow_Exception()
        {
            Action action = () => ContainerBuilderExtensions.RegisterTypeAsInstancePerDependency<Foo, IFoo>(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should resolve instance or not.
        /// </summary>
        [Fact]
        public void Given_Instance_RegisterTypeAsInstancePerDependency_ShouldReturn_Result()
        {
            var resolved = this._fixture.ArrangeFixturesForRegisterTypeAsInstancePerDependency<Foo, IFoo>();

            resolved.Should().BeOfType<Foo>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_RegisterTypeAsSingleInstance_ShouldThrow_Exception()
        {
            Action action = () => ContainerBuilderExtensions.RegisterTypeAsSingleInstance<Foo, IFoo>(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should resolve instance or not.
        /// </summary>
        [Fact]
        public void Given_Instance_RegisterTypeAsSingleInstance_ShouldReturn_Result()
        {
            var resolved = this._fixture.ArrangeFixturesForRegisterTypeAsSingleInstance<Foo, IFoo>();

            resolved.Should().BeOfType<Foo>();
        }
    }
}
