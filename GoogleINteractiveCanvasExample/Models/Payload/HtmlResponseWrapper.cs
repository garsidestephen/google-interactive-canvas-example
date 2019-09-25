using Newtonsoft.Json;

namespace GoogleInteractiveCanvasExample.Models.Payload
{
    /// <summary>
    /// HtmlResponse Wrapper
    /// </summary>
    public class HtmlResponseWrapper
    {
        /// <summary>
        /// Initialises a new instance of HtmlResponseWrapper
        /// </summary>
        public HtmlResponseWrapper()
        {
            HtmlResponse = new HtmlResponse();
        }

        /// <summary>
        /// Gets or sets HtmlResponse
        /// </summary>
        [JsonProperty(PropertyName = "htmlResponse")]
        public HtmlResponse HtmlResponse { get; set; }
    }
}