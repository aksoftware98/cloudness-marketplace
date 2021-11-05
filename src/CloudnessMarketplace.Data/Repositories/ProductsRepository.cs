using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Data.Options;
using CloudnessMarketplace.Models;
using CloudnessMarketplace.Shared.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {

        private readonly Database _db;
        private const string CONTAINER_NAME = "Products";
        private readonly Container _container; 

        public ProductsRepository(DbOptions options)
        {
            var cosmosClient = new CosmosClient(options.ConnectionString);
            _db = cosmosClient.GetDatabase(options.DatabaseName);
            _container = _db.GetContainer(CONTAINER_NAME); 
        }

        public async Task<Product> CreateAsync(Product product)
        {
            // Validate the fields 
            var result = await _container.CreateItemAsync(product);
            return result; 
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var query = $"SELECT * FROM p WHERE p.id = '{id}'";

            var iterator = _container.GetItemQueryIterator<Product>(query);
            var result = await iterator.ReadNextAsync();
            if (result.Resource.Any())
                return result.FirstOrDefault();

            return null;
        }

        public async Task DeleteAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            await _container.DeleteItemAsync<Product>(product.Id, new PartitionKey(product.Category)); 
        }

        public async Task<PagedList<Product>> GetFeaturedProductsAsync(int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Product>> GetProductsByCategoryAsync(string categoryName, int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Product>> GetTodayProductsAsync(int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Product>> GetUserProductsAsync(string userId, int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task SellAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }


}
