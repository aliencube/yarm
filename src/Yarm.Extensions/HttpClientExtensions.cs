using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Yarm.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="HttpClient"/> class.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Sends the <c>PATCH</c> request.
        /// </summary>
        /// <typeparam name="T">Type of the instance as a payload.</typeparam>
        /// <param name="client"><see cref="HttpClient"/> instance.</param>
        /// <param name="requestUri">Request Uri.</param>
        /// <param name="value">Payload instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T value)
        {
            client.ThrowIfNullOrDefault();
            requestUri.ThrowIfNullOrWhiteSpace();
            value.ThrowIfNullOrDefault();

            using (var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json"))
            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = content })
            {
                var response = await client.SendAsync(request).ConfigureAwait(false);

                return response;
            }
        }
    }
}
