using Newtonsoft.Json;

namespace GoogleInteractiveCanvasExample.Models.Payload
{
    /// <summary>
    /// Simple Response
    /// </summary>
    public class SimpleResponse
    {
        /// <summary>
        /// Gets or sets TextToSpeech
        /// </summary>
        [JsonProperty(PropertyName = "textToSpeech")]
        public string TextToSpeech { get; set; }
    }
}