using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;
using CloudnessMarketplace.Data.Interfaces;

namespace Server
{
    public class UploadFile
    {
        private readonly IConfiguration _config;
        private readonly IPictureRepository _picsRepo;

        public UploadFile(IConfiguration config, IPictureRepository picsRepo)
        {
            _config = config;
            _picsRepo = picsRepo;
        }

        [FunctionName("UploadFile")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Upload File Called");
            log.LogInformation($"{_config["AzureWebJobsStorage"]}");
            var formFile = req.Form.Files["file"];
            if (formFile == null)
                return new BadRequestObjectResult(new
                {
                    message = "File is required"
                });

            // Validate the file 
            var extension = Path.GetExtension(formFile.FileName);
            var allowedExtensions = new[] { ".jpg", ".png", ".svg", ".hiec" };

            if (!allowedExtensions.Contains(extension))
                return new BadRequestObjectResult(new
                {
                    message = "Invalid image file"
                });

            // Validate the size of the file 
            if (formFile.Length > (1024 * 1024 * 10))
                return new BadRequestObjectResult(new
                {
                    message = "Image file cannot be more than 10MB"
                });

            var storageClient = StorageAccount.NewFromConnectionString(_config["AzureWebJobsStorage"]);
            var blobClient = storageClient.CreateCloudBlobClient();
            var containerClient = blobClient.GetContainerReference("pictures");
            await containerClient.CreateIfNotExistsAsync();
            string newName = $"{Guid.NewGuid()}{extension}";
            var blockBlobClient = containerClient.GetBlockBlobReference(newName);

            string url = string.Empty; 
            using (var stream = formFile.OpenReadStream())
            {
                await blockBlobClient.UploadFromStreamAsync(stream);
                url = $"{_config["BlobContainerUrl"]}/{newName}";
                // Save the file to the database
                // TODO: Bring the User Id 
                await _picsRepo.AddAsync(url, formFile.FileName, stream.Length, "userId");
            }
            return new OkObjectResult(new
            {
                message = "File uploaded successfully",
                data = url
            });
        }
    }
}
