using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Data.Options;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Repositories
{
    public class CosmosDbDataProvider : IDatabaseProvider
    {

        private readonly DbOptions _options;
        private readonly Database _db;

        public CosmosDbDataProvider(DbOptions options)
        {
            _options = options;
            var cosmosClient = new CosmosClient(options.ConnectionString);
            _db = cosmosClient.GetDatabase(options.DatabaseName);
        }

        /// <summary>
        /// Create the required containers
        /// </summary>
        /// <returns></returns>
        public async Task InitializeDbAsync()
        {
            await _db.CreateContainerIfNotExistsAsync("Pictures", "/type");
            await _db.CreateContainerIfNotExistsAsync("ProductLikes", "/userid");
            await _db.CreateContainerIfNotExistsAsync("Products", "/category");
            await _db.CreateContainerIfNotExistsAsync("ProductViews", "/category");
        }
    }
}
