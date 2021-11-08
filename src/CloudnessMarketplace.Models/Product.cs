using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudnessMarketplace.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description {  get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string , object> Metadata { get; set; }

        [JsonProperty("pictureUrls")]
        public List<string> PictureUrls { get; set; }

        [JsonProperty("isSold")]
        public bool IsSold { get; set; }

        [JsonProperty("sellingDate")]
        public DateTime? SellingDate { get; set; }
    
        [JsonProperty("likes")]
        public int Likes { get; set; }

        [JsonProperty("views")]
        public int Views { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }

}
