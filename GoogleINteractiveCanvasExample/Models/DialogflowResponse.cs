using Newtonsoft.Json;

namespace GoogleInteractiveCanvasExample.Models
{
    /// <summary>
    /// Dialogflow Response
    /// </summary>
    public class DialogflowResponse
    {    
        /// <summary>
        /// Gets or sets Payload Holder
        /// </summary>
        [JsonProperty(PropertyName = "payload", NullValueHandling = NullValueHandling.Ignore)]
        public PayloadWrapper Payload { get; set; }
    }
}