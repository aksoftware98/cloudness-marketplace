using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Shared.Responses;
using System.Collections.Generic;
using CloudnessMarketplace.Models;
using System.Linq;

namespace CloudnessMarketplace.Functions
{
    public class RemovePendingPictures
    {

        private readonly IPictureRepository _picturesRepo;

        public RemovePendingPictures(IPictureRepository picturesRepo)
        {
            _picturesRepo = picturesRepo;
        }

        [FunctionName("RemovePendingPictures")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Retrieving the list of pending images");

            var result = await _picturesRepo.ListPendingAsync();

            if (result.Any())
            {
                log.LogInformation($"{result.Count()} picture(s) have been found to remove");
                foreach (var item in result)
                {
                    await _picturesRepo.RemoveAsync(item.Url);
                }
                log.LogInformation($"{result.Count()} picture(s) have been removed successfully!");
            }

            return new OkObjectResult(new ApiResponse($"{result.Count()} image has been removed successfully!", true));
        }
    }
}
