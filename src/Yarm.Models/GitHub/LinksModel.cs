namespace Yarm.Models.GitHub
{
    /// <summary>
    /// This represents the model entity for GitHub content links.
    /// </summary>
    public class LinksModel
    {
        /// <summary>
        /// Gets or sets the self link.
        /// </summary>
        public string Self { get; set; }

        /// <summary>
        /// Gets or sets the git link.
        /// </summary>
        public string Git { get; set; }

        /// <summary>
        /// Gets or sets the HTML link.
        /// </summary>
        public string Html { get; set; }
    }
}