using GoogleInteractiveCanvasExample.Models.Payload;
using Newtonsoft.Json;

namespace GoogleInteractiveCanvasExample.Models
{
    /// <summary>
    /// Payload Wrapper
    /// </summary>
    public class PayloadWrapper
    {
        /// <summary>
        /// Initialises a new instance of PayloadWrapper
        /// </summary>
        public PayloadWrapper()
        {
            Google = new GooglePayload();
        }

        /// <summary>
        /// Gets or sets GooglePayload
        /// </summary>
        [JsonProperty(PropertyName = "google", NullValueHandling = NullValueHandling.Ignore)]
        public GooglePayload Google { get; set; }
    }
}