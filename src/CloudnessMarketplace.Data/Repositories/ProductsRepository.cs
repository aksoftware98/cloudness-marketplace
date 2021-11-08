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

        public async Task<Product> GetByIdAsync(string id, string userId = null, bool increaseView = false)
        {
            var query = $"SELECT * FROM p WHERE p.id = '{id}'";

            var iterator = _container.GetItemQueryIterator<Product>(query);
            var result = await iterator.ReadNextAsync();
            if (result.Resource.Any())
            {
                var product = result.FirstOrDefault();
                await HandleProductViewAsync(increaseView, product, userId);

                return product;
            }

            return null;
        }



        public async Task DeleteAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            await _container.DeleteItemAsync<Product>(product.Id, new PartitionKey(product.Category));
        }

        public async Task<PagedList<Models.ProductSummary>> GetTrendProductsAsync(int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Models.ProductSummary>> GetProductsByCategoryAsync(string categoryName, int pageIndex = 1, int pageSize = 10)
        {
            if (pageSize > 100)
                pageSize = 100;

            if (pageSize < 5)
                pageSize = 5;

            if (pageIndex < 1)
                pageIndex = 1;

            // Get the total count of the items 
            var countQuery = $"SELECT VALUE COUNT('id') FROM c WHERE c.category = '{categoryName}'";

            var countsIterator = _container.GetItemQueryIterator<int>(countQuery);
            var countsResult = await countsIterator.ReadNextAsync();
            int totalCount = countsResult.Resource.FirstOrDefault();

            // Get all the items within the category 
            int skip = (pageIndex - 1) * pageSize;
            int limit = pageSize;
            var query = $"SELECT c.id, c.name, c.pictureUrls[0] as cover, c.userId, c.likes, c.views, c.price, c.isSold, c.creationDate, c.category FROM c WHERE c.category = '{categoryName}' OFFSET {skip} LIMIT {limit}";
            var iterator = _container.GetItemQueryIterator<Models.ProductSummary>(query);
            var result = await iterator.ReadNextAsync();

            int totalPages = totalCount / pageSize;

            if (totalCount % pageSize != 0)
                totalPages++;

            return new PagedList<Models.ProductSummary>(result.Resource, pageIndex, totalPages, pageSize, totalCount);
        }

        public Task<PagedList<Models.ProductSummary>> GetTodayProductsAsync(int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Models.ProductSummary>> GetUserProductsAsync(string userId, int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public async Task SellAsync(Product product)
        {
            product.IsSold = true;
            product.SellingDate = DateTime.UtcNow;

            await _container.ReplaceItemAsync<Product>(product, product.Id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            var result = await _container.ReplaceItemAsync<Product>(product, product.Id);
            return result.Resource;
        }

        #region Helper Methods
        private async Task HandleProductViewAsync(bool increaseView, Product product, string userId)
        {

            if (increaseView)
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product));
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId));
                // Increase the total number of the views
                product.Views++;
                await _container.ReplaceItemAsync<Product>(product, product.Id);

                // Add the view object to track the time and who viewed the prdocut 
                await AddProductViewAsync(product, userId);
            }
        }

        /// <summary>
        /// Add <see cref="ProductView"/> object to the database so we can track who viewed the product and when
        /// </summary>
        /// <param name="product">Product that has been viewed</param>
        /// <returns></returns>
        private async Task AddProductViewAsync(Product product, string userId)
        {
            var viewsContainer = _db.GetContainer("ProductViews");
            await viewsContainer.CreateItemAsync<ProductView>(new ProductView
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product.Id,
                UserId = userId,
                ViewDate = DateTime.UtcNow,
            });
        }
        #endregion 
    }


}
