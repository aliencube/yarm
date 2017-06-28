namespace Yarm.Models.Functions.Responses
{
    /// <summary>
    /// This represents the model entity for error response.
    /// </summary>
    public class ErrorResponseModel
    {
        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        public string Description { get; set; }
    }
}
