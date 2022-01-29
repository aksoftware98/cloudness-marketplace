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
using CloudnessMarketplace.Shared.Models;
using CloudnessMarketplace.Functions.Extensions;

namespace CloudnessMarketplace.Functions
{
    public class GetProductDetails
    {

        private readonly IProductsRepository _productsRepo;
        private readonly IProductLikesRepository _likesRepo;

        public GetProductDetails(IProductsRepository productsRepo, IProductLikesRepository likesRepo)
        {
            _productsRepo = productsRepo;
            _likesRepo = likesRepo;
        }

        [FunctionName("GetProductDetails")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get product details by Id");

            // Get the user id from the access token
            string userId = req.GetUserId();
            string productId = req.Query["productId"];
            if (productId == null)
                return new NotFoundResult();

            string increaseViewString = req.Query["increaseView"];
            bool increaseView = false;
            if (!bool.TryParse(increaseViewString, out increaseView))
                increaseView = false;

            var product = await _productsRepo.GetByIdAsync(productId, userId, increaseView);

            if (product == null)
                return new NotFoundResult();

            var productLike = await _likesRepo.GetProductLikeAsync(product.Id, userId);

            return  new OkObjectResult(new ApiResponse<ProductDto>("Product retrieved successfully", new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                CreationDate = product.CreationDate,
                Description = product.Description,
                IsSold = product.IsSold,
                Likes = product.Likes,
                Metadata = product.Metadata,
                PictureUrls = product.PictureUrls,
                Price = product.Price,
                SellingDate = product.SellingDate,
                UserId = product.UserId,
                Views = product.Views,
                IsLikedByCurrentUser = productLike != null
            }));
        }
    }
}
