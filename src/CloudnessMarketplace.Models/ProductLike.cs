using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace CloudnessMarketplace.Models
{
    public class ProductLike
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("likeDate")]
        public DateTime LikeDate { get; set; }
    }

}
