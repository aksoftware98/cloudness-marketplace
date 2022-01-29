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
using CloudnessMarketplace.Functions.Extensions;

namespace CloudnessMarketplace.Functions
{
    public class SellProduct
    {

        private readonly IProductsRepository _productsRepo;

        public SellProduct(IProductsRepository productsRepo)
        {
            _productsRepo = productsRepo;
        }

        [FunctionName("SellProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Sell product");

            // Get the user id 
            string userId = req.GetUserId(); 
            // TODO: Validate the user id with the owern of the product 

            string id = req.Query["id"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            id = id ?? data?.id;

            if (id == null)
                return new NotFoundResult();

            var product = await _productsRepo.GetByIdAsync(id);
            if (product == null)
                return new NotFoundResult();

            await _productsRepo.SellAsync(product);

            return new OkObjectResult(new ApiResponse("Product has been sold successfully!", true));
        }
    }
}
