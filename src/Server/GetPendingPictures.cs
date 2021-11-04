using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Shared.Responses;
using System.Collections.Generic;
using CloudnessMarketplace.Models;

namespace CloudnessMarketplace.Functions
{
    public class GetPendingPictures
    {

        private readonly IPictureRepository _picturesRepo;

        public GetPendingPictures(IPictureRepository picturesRepo)
        {
            _picturesRepo = picturesRepo;
        }

        [FunctionName("GetPendingPictures")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Retrieving the list of pending images");

            var result = await _picturesRepo.ListPendingAsync();

            return new OkObjectResult(new ApiResponse<IEnumerable<Picture>>("Retrieving the list of messages", result));
        }
    }
}
