using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudnessMarketplace.Data.Interfaces;

namespace CloudnessMarketplace.Functions
{
    public class SetupDatabaseContainers
    {

        private readonly IDatabaseProvider _dbProvider;

        public SetupDatabaseContainers(IDatabaseProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        [FunctionName("SetupDatabaseContainers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Setup database containers");

            await _dbProvider.InitializeDbAsync();

            log.LogInformation("Database containers setup successfully!");

            return new OkResult();
        }
    }
}
