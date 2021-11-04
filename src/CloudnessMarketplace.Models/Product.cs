using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudnessMarketplace.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description {  get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string , object> Metadata { get; set; }

        [JsonPropertyName("pictureUrls")]
        public List<string> PictureUrls { get; set; }

        [JsonPropertyName("isSold")]
        public bool IsSold { get; set; }

        [JsonPropertyName("sellingDate")]
        public DateTime? SellingDate { get; set; }

    }


}
