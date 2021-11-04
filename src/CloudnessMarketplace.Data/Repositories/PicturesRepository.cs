using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Data.Options;
using CloudnessMarketplace.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Repositories
{
    public class PicturesRepository : IPictureRepository
    {

        private readonly Database _db;
        private const string CONTAINER_NAME = "Pictures";
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

        public Task<IEnumerable<Picture>> ListPendingAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
