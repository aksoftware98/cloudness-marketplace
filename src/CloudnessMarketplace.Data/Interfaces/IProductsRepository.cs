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
        Task<Product> GetByIdAsync(string id, string userId = null, bool increaseView = false);
        Task<PagedList<Models.ProductSummary>> GetProductsByCategoryAsync(string categoryName, int pageIndex = 1, int pageSize = 10);
        Task<PagedList<Models.ProductSummary>> GetUserProductsAsync(string userId, int pageIndex = 1, int pageSize = 10);
        Task<PagedList<Models.ProductSummary>> GetTrendProductsAsync(int pageIndex = 1, int pageSize = 10);
        Task<PagedList<Models.ProductSummary>> GetTodayProductsAsync(int pageIndex = 1, int pageSize = 10);
    }
}
