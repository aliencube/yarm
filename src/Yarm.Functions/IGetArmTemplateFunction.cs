using Yarm.Functions.FunctionFactories;

namespace Yarm.Functions
{
    /// <summary>
    /// This provides interfaces to the <see cref="GetArmTemplateFunction"/> class.
    /// </summary>
    public interface IGetArmTemplateFunction : IFunction
    {
        /// <summary>
        /// Gets or sets the template directory name.
        /// </summary>
        string TemplateName { get; set; }
    }
}