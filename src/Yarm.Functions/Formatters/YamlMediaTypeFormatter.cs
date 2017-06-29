using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yarm.Functions.Formatters
{
    /// <summary>
    /// This represents the formatter entity for "application/yaml" and "application/x-yaml" mime type.
    /// </summary>
    public class YamlMediaTypeFormatter : MediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YamlMediaTypeFormatter"/> class.
        /// </summary>
        public YamlMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-yaml"));
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/yaml"));
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/yaml"));
        }

        /// <inheritdoc/>
        public override bool CanWriteType(Type type)
        {
            return type == typeof(string);
        }

        /// <inheritdoc/>
        public override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }

        /// <inheritdoc/>
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
        {
            var buff = Encoding.UTF8.GetBytes(value.ToString());
            return writeStream.WriteAsync(buff, 0, buff.Length, cancellationToken);
        }
    }
}
