using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Data.Options;
using CloudnessMarketplace.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Repositories
{
    public class PicturesRepository : IPictureRepository
    {

        private readonly Database _db;
        private const string CONTAINER_NAME = "Pictures";
        private const int MAX_HOURS = 24;

        public PicturesRepository(DbOptions options)
        {
            var cosmosClient = new CosmosClient(options.ConnectionString);
            _db = cosmosClient.GetDatabase(options.DatabaseName);
        }

        public async Task<string> AddAsync(string url, string name, long fileSize, string userId)
        {
            var picture = new Picture
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Size = fileSize,
                UserId = userId,
                CreationDate = DateTime.UtcNow,
                Url = url,
                Type = Path.GetExtension(url).Replace(".", "").ToLower()
            };

            // Get the container 
            var container = _db.GetContainer(CONTAINER_NAME);
            await container.CreateItemAsync(picture);
            return picture.Url; 
        }

        public async Task<Picture> GetByUrlAsync(string url)
        {
            var query = $"SELECT * FROM c WHERE c.url = {url}";


            // Get the container 
            var container = _db.GetContainer(CONTAINER_NAME);
            var iterator = container.GetItemQueryIterator<Picture>(query);
            var result = await iterator.ReadNextAsync();
            if (result.Resource.Any())
                return result.Resource.FirstOrDefault();

            return null;
        }

        public async Task<IEnumerable<Picture>> ListPendingAsync()
        {
            var query = $"SELECT * FROM c WHERE c.creationDate > '{DateTime.UtcNow.AddHours(MAX_HOURS)}'";

            // Get the container 
            var container = _db.GetContainer(CONTAINER_NAME);
            var iterator = container.GetItemQueryIterator<Picture>(query);
            var result = await iterator.ReadNextAsync();
            var pictures = new List<Picture>();
            pictures.AddRange(result.Resource);

            while (result.ContinuationToken != null)
            {
                iterator = container.GetItemQueryIterator<Picture>(query, result.ContinuationToken);
                result = await iterator.ReadNextAsync();
                pictures.AddRange(result.Resource);
            }

            return pictures;
        }

        public async Task RemoveAsync(string id)
        {
            // Get the container 
            var container = _db.GetContainer(CONTAINER_NAME);

            await container.DeleteItemAsync<Picture>(id, new PartitionKey());
        }
    }
}
