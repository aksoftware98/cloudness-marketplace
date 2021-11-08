using Newtonsoft.Json;
using System;

namespace CloudnessMarketplace.Models
{
    public class ProductSummary
    {
        [JsonProperty("id")]
        public string Id {  get; set; }

        [JsonProperty("name")]
        public string Name {  get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("likes")]
        public int Likes { get; set; }

        [JsonProperty("views")]
        public int Views { get; set; }

        [JsonProperty("isSold")]
        public bool IsSold { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("cover")]
        public string Cover { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }
    }

}
