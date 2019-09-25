using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace GoogleInteractiveCanvasExample.Models.Payload
{
    /// <summary>
    /// Rich Response
    /// </summary>
    public class RichResponse
    {
        /// <summary>
        /// Initialises a new Instance of a Rich Response
        /// </summary>
        public RichResponse()
        {
            Items = new Collection<object>();
        }

        /// <summary>
        /// Gets or sets Items
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public Collection<object> Items { get; }
    }
}