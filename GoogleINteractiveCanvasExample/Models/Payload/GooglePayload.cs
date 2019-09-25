using Newtonsoft.Json;

namespace GoogleInteractiveCanvasExample.Models.Payload
{
    public class GooglePayload
    {
        public GooglePayload()
        {
            RichResponse = new RichResponse();
        }

        [JsonProperty(PropertyName = "expectUserResponse")]
        public bool ExpectUserResponse { get; set; }

        [JsonProperty(PropertyName = "richResponse")]
        public RichResponse RichResponse { get; set; }
    }
}