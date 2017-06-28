using System;
using System.Net.Http;
using System.Threading.Tasks;

using Yarm.Functions.FunctionFactories;

namespace Yarm.Functions.Tests.Fixtures
{
    /// <summary>
    /// This provides interfaces to the <see cref="FooFunction"/> class.
    /// </summary>
    public interface IFooFunction : IFunction
    {
        /// <summary>
        /// Gets or sets the bar.
        /// </summary>
        string Bar { get; set; }
    }

    /// <summary>
    /// This represents the Foo function entity.
    /// </summary>
    public class FooFunction : FunctionBase, IFooFunction
    {
        /// <summary>
        /// Gets or sets the bar.
        /// </summary>
        public string Bar { get; set; }

        /// <inheritdoc />
        public override Task<HttpResponseMessage> InvokeAsync(HttpRequestMessage req)
        {
            throw new NotImplementedException();
        }
    }
}
