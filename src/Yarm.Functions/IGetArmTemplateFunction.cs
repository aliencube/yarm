using Yarm.Functions.FunctionFactories;

namespace Yarm.Functions
{
    /// <summary>
    /// This provides interfaces to the <see cref="GetArmTemplateFunction"/> class.
    /// </summary>
    public interface IGetArmTemplateFunction : IFunction
    {
        /// <summary>
        /// Gets the <see cref="GetArmTemplateFunctionParameterOptions"/> instance.
        /// </summary>
        GetArmTemplateFunctionParameterOptions Parameters { get; }
    }
}