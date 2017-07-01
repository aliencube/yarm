namespace Yarm.Functions.FunctionFactories
{
    /// <summary>
    /// This represents the options entity for the <see cref="GetArmTemplateFunction"/> parameters.
    /// </summary>
    public class GetArmTemplateFunctionParameterOptions : FunctionParameterOptions
    {
        /// <summary>
        /// Gets or sets the template name.
        /// </summary>
        public string TemplateName { get; set; }
    }
}