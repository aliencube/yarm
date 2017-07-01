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
        /// Adds <see cref="FunctionParameterOptions"/> instance.
        /// </summary>
        /// <typeparam name="TFunction">Type of function.</typeparam>
        /// <typeparam name="TOptions">Type of parameter options.</typeparam>
        /// <param name="instance">Function instance.</param>
        /// <param name="options">Options instance.</param>
        /// <returns>Returns the function instance with the parameter options added.</returns>
        public static TFunction AddParameters<TFunction, TOptions>(this TFunction instance, TOptions options)
            where TFunction : IFunction
            where TOptions : FunctionParameterOptions
        {
            instance.ThrowIfNullOrDefault();
            options.ThrowIfNullOrDefault();

            instance.ParameterOptions = options;

            return instance;
        }
    }
}
