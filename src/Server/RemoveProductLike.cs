using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Shared.Responses;
using CloudnessMarketplace.Functions.Extensions;

namespace CloudnessMarketplace.Functions
{
    public class RemoveProductLike
    {

        private readonly IProductLikesRepository _productLikesRepo;

        public RemoveProductLike(IProductLikesRepository productLikesRepo)
        {
            _productLikesRepo = productLikesRepo;
        }

        [FunctionName("DeleteProductLike")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("Delete like product");

            string userId = req.GetUserId();
            if (userId == null)
                return new UnauthorizedResult();
            string productId = req.Query["productId"];

            if (productId == null)
                return new NotFoundResult();

            await _productLikesRepo.RemoveLikeAsync(productId, userId);

            return new OkObjectResult(new ApiResponse("Delete product like succeeded", true));
        }
    }
}
