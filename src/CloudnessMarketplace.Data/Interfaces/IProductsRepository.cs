using CloudnessMarketplace.Models;
using CloudnessMarketplace.Shared.Models;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task SellAsync(Product product);
        Task<Product> GetByIdAsync(string id);
        Task<PagedList<Product>> GetProductsByCategoryAsync(string categoryName, int pageIndex = 1, int pageSize = 10);
        Task<PagedList<Product>> GetUserProductsAsync(string userId, int pageIndex = 1, int pageSize = 10);
        Task<PagedList<Product>> GetFeaturedProductsAsync(int pageIndex = 1, int pageSize = 10);
        Task<PagedList<Product>> GetTodayProductsAsync(int pageIndex = 1, int pageSize = 10);
    }
}
