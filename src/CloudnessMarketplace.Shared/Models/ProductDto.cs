using System;
using System.Collections.Generic;

namespace CloudnessMarketplace.Shared.Models
{
    public class ProductDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public DateTime CreationDate { get; set; }

        public Dictionary<string, object> Metadata { get; set; }

        public List<string> PictureUrls { get; set; }

        public bool IsSold { get; set; }

        public DateTime? SellingDate { get; set; }
    }
}
