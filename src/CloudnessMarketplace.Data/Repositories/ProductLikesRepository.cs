﻿using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Data.Options;
using CloudnessMarketplace.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Repositories
{
    public class ProductLikesRepository : IProductLikesRepository
    {
        private readonly Database _db;
        private const string CONTAINER_NAME = "ProductLikes";
        private readonly Container _container;
        private readonly IProductsRepository _productsRepo;
        public ProductLikesRepository(DbOptions options,
                                      IProductsRepository productsRepo)
        {
            var cosmosClient = new CosmosClient(options.ConnectionString);
            _db = cosmosClient.GetDatabase(options.DatabaseName);
            _container = _db.GetContainer(CONTAINER_NAME);
            _productsRepo = productsRepo;
        }

        public async Task LikeProductAsync(string productId, string userId)
        {
            // Get the item by id 
            var product = await _productsRepo.GetByIdAsync(productId, false);
            if (product == null)
                return; 

            await _container.CreateItemAsync<ProductLike>(new ProductLike
            {
                Id = Guid.NewGuid().ToString(),
                LikeDate = DateTime.UtcNow,
                ProductId = productId,
                UserId = userId,
            });

            product.Likes++;
            await _productsRepo.UpdateAsync(product);
        }

        public async Task RemoveLikeAsync(string productId, string userId)
        {
            // Get the product
            var product = await _productsRepo.GetByIdAsync(productId, false);
            if (product == null)
                return; 

            product.Likes--;
            
            var query = $"SELECT * FROM c WHERE c.productId = '{productId}' AND c.userId = '{userId}'";
            var iterator = _container.GetItemQueryIterator<ProductLike>(query);
            var result = await iterator.ReadNextAsync();
            if (result.Resource.Any())
            {
                await _container.DeleteItemAsync<ProductLike>(result.Resource.First().Id, new PartitionKey(userId));
                await _productsRepo.UpdateAsync(product);
            }

        }
    }


}
