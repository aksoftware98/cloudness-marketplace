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
    public class LikeProduct
    {

        private readonly IProductLikesRepository _productLikesRepo;

        public LikeProduct(IProductLikesRepository productLikesRepo)
        {
            _productLikesRepo = productLikesRepo;
        }

        [FunctionName("LikeProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Like product");

            string userId = req.GetUserId();
            string productId = req.Query["productId"];

            if (productId == null)
                return new NotFoundResult();

            await _productLikesRepo.LikeProductAsync(productId, userId);

            return new OkObjectResult(new ApiResponse("Product liked successfully!", true));
        }
    }
}
