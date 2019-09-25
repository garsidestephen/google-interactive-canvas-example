using Newtonsoft.Json;

namespace GoogleInteractiveCanvasExample.Models.Payload
{
    /// <summary>
    /// Updated State
    /// </summary>
    public class UpdatedState
    {
        /// <summary>
        /// Gets or sets Command
        /// </summary>
        [JsonProperty(PropertyName = "command")]
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets Arguments
        /// </summary>
        [JsonProperty(PropertyName = "arguments", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Arguments { get; set; }
    }
}