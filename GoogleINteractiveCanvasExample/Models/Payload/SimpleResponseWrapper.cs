using Newtonsoft.Json;

namespace GoogleInteractiveCanvasExample.Models.Payload
{
    /// <summary>
    /// Simple Response Wrapper
    /// </summary>
    public class SimpleResponseWrapper
    {
        /// <summary>
        /// Simple Response Wrapper
        /// </summary>
        public SimpleResponseWrapper()
        {
            SimpleResponse = new SimpleResponse();
        }

        /// <summary>
        /// Gets or sets SimpleResponse
        /// </summary>
        [JsonProperty(PropertyName = "simpleResponse")]
        public SimpleResponse SimpleResponse { get; set; }
    }
}