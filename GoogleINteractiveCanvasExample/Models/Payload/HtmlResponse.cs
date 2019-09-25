using Newtonsoft.Json;

namespace GoogleInteractiveCanvasExample.Models.Payload
{
    /// <summary>
    /// Html Response
    /// </summary>
    public class HtmlResponse
    {
        /// <summary>
        /// Gets or sets the Interactive Canvas Url
        /// </summary>
        [JsonProperty(PropertyName = "url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        /// <summary>
        /// Updated State
        /// </summary>
        [JsonProperty(PropertyName = "updatedState", NullValueHandling = NullValueHandling.Ignore)]
        public UpdatedState UpdatedState { get; set; }
    }
}