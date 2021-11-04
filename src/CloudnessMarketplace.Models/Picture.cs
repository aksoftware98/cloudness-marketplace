using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace CloudnessMarketplace.Models
{
    public class Picture
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("userId")]
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonProperty("creationDate")]
        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("type")]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonProperty("size")]
        [JsonPropertyName("size")]
        public long Size { get; set; }
    }


}
