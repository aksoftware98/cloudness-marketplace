﻿using System.Text.Json.Serialization;

namespace CloudnessMarketplace.Models
{
    public class Category
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("iconUrl")]
        public string IconUrl { get; set; }
    }

}
