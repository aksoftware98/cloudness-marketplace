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
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CloudnessMarketplace.Functions
{
    public class RemovePendingPictures
    {

        private readonly IPictureRepository _picturesRepo;
        private readonly IConfiguration _configuration;

        public RemovePendingPictures(IPictureRepository picturesRepo, IConfiguration configuration)
        {
            _picturesRepo = picturesRepo;
            _configuration = configuration;
        }

        [FunctionName("RemovePendingPictures")]
        public async Task Run([TimerTrigger("0 0 * * * *")] TimerInfo timer, ILogger log)
        {
            log.LogInformation("Retrieving the list of pending images");

            var result = await _picturesRepo.ListPendingAsync();

            var storageAccount = StorageAccount.NewFromConnectionString("AzureWebJobsStorage");
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("pictures");

            if (result.Any())
            {
                log.LogInformation($"{result.Count()} picture(s) have been found to remove");
                foreach (var item in result)
                {
                    await _picturesRepo.RemoveAsync(item.Url);

                    // Remove the image from the blob storage 
                    var blob = container.GetBlockBlobReference(Path.GetFileName(item.Url));
                    await blob.DeleteIfExistsAsync();
                }
                log.LogInformation($"{result.Count()} picture(s) have been removed successfully!");
            }

        }
    }
}
