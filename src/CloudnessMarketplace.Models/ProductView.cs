using System.Text.Json.Serialization;

namespace CloudnessMarketplace.Models
{
    public class ProductView
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("productId")]
        public string ProductId { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }


}
