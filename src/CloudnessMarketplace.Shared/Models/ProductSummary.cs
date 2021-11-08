using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Shared.Models
{
    public class ProductSummary
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public int Likes { get; set; }

        public int Views { get; set; }

        public bool IsSold { get; set; }

        public string UserId { get; set; }

        public string Cover { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
