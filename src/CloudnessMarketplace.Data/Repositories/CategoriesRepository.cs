using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return Task.FromResult(new[]
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Shoes",
                    IconUrl = ""
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Mobiles",
                    IconUrl = ""
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Laptops",
                    IconUrl = ""
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "T-Shirts",
                    IconUrl = ""
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Sports Wear",
                    IconUrl = ""
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Toys",
                    IconUrl = ""
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Accessories",
                    IconUrl = ""
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Cars",
                    IconUrl = ""
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Sunglasses",
                    IconUrl = ""
                }
            }.AsEnumerable());
        }
    }
}
