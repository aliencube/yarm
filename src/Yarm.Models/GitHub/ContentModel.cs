using Newtonsoft.Json;

namespace Yarm.Models.GitHub
{
    /// <summary>
    /// This represents the model entity for GitHub content.
    /// </summary>
    public class ContentModel
    {
        /// <summary>
        /// Gets or sets the content name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the content path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the SHA value.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// Gets or sets the content size.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the content URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the HTML URL of the content.
        /// </summary>
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        /// <summary>
        /// Gets or sets the git URL of the content.
        /// </summary>
        [JsonProperty("git_url")]
        public string GitUrl { get; set; }

        /// <summary>
        /// Gets or sets the download URL of the content.
        /// </summary>
        [JsonProperty("download_url")]
        public object DownloadUrl { get; set; }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        [JsonProperty("type")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LinksModel"/> instance.
        /// </summary>
        [JsonProperty("_links")]
        public LinksModel Links { get; set; }
    }
}
