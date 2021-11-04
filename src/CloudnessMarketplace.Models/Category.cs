using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CloudnessMarketplace.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }
    }

}
