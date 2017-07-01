using System;
using System.Net.Http;
using System.Threading.Tasks;

using Yarm.Functions.FunctionFactories;

namespace Yarm.Functions.Tests.Fixtures
{
    /// <summary>
    /// This represents the parameter options entity for the <see cref="FooFunction"/> class.
    /// </summary>
    public class FooFunctionParameterOptions : FunctionParameterOptions
    {
        /// <summary>
        /// Gets or sets the Bar value.
        /// </summary>
        public string Bar { get; set; }
    }

    /// <summary>
    /// This provides interfaces to the <see cref="FooFunction"/> class.
    /// </summary>
    public interface IFooFunction : IFunction
    {
        /// <summary>
        /// Gets the <see cref="FooFunctionParameterOptions"/> instance.
        /// </summary>
        FooFunctionParameterOptions Parameters { get; }
    }

    /// <summary>
    /// This represents the Foo function entity.
    /// </summary>
    public class FooFunction : FunctionBase, IFooFunction
    {
        /// <summary>
        /// Gets the <see cref="FooFunctionParameterOptions"/> instance.
        /// </summary>
        public FooFunctionParameterOptions Parameters => this.ParameterOptions as FooFunctionParameterOptions;

        /// <inheritdoc />
        public override Task<HttpResponseMessage> InvokeAsync(HttpRequestMessage req)
        {
            throw new NotImplementedException();
        }
    }
}
