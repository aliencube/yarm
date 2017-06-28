using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;

using Yarm.Functions.ServiceLocators;

namespace Yarm.Functions.FunctionFactories
{
    /// <summary>
    /// This represents the factory entity for functions.
    /// </summary>
    public class FunctionFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionFactory"/> class.
        /// </summary>
        public FunctionFactory()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionFactory"/> class.
        /// </summary>
        /// <param name="handler"><see cref="RegistrationHandler"/> instance.</param>
        public FunctionFactory(RegistrationHandler handler = null)
        {
            this.ServiceLocator = new ServiceLocatorBuilder()
                                      .RegisterModule<AppModule>(handler)
                                      .Build();
        }

        /// <summary>
        /// Gets or sets the <see cref="IServiceLocator"/> instance.
        /// </summary>
        public virtual IServiceLocator ServiceLocator { get; set; }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <typeparam name="TFunction">The type of the function.</typeparam>
        /// <param name="log">A <see cref="ILogger"/> instance for tracing.</param>
        /// <returns>The function.</returns>
        public virtual TFunction Create<TFunction>(ILogger log)
            where TFunction : IFunction
        {
            var function = this.ServiceLocator.GetInstance<TFunction>();
            function.Log = log;
            function.ServiceLocator = this.ServiceLocator;

            return function;
        }
    }
}
