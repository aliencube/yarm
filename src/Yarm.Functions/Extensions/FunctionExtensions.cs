using System;

using Yarm.Extensions;
using Yarm.Functions.FunctionFactories;

namespace Yarm.Functions.Extensions
{
    /// <summary>
    /// This represents the extension entity for functions.
    /// </summary>
    public static class FunctionExtensions
    {
        /// <summary>
        /// Loads a property to the given instance.
        /// </summary>
        /// <typeparam name="TFunction">Type of function.</typeparam>
        /// <param name="instance">Function instance.</param>
        /// <param name="action">Action expression.</param>
        /// <returns>Returns the function instance with the property loaded.</returns>
        public static TFunction LoadProperty<TFunction>(this TFunction instance, Action<TFunction> action)
            where TFunction : IFunction
        {
            instance.ThrowIfNullOrDefault();
            action.ThrowIfNullOrDefault();

            action.Invoke(instance);

            return instance;
        }
    }
}
