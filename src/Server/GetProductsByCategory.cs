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
using System.Linq;

namespace CloudnessMarketplace.Functions
{
    public class GetProductsByCategory
    {

        private readonly IProductsRepository _productsRepository;

        public GetProductsByCategory(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [FunctionName("GetProductsByCategory")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get products by category");

            string name = req.Query["category"];
            string pageNumberAsString = req.Query["pageIndex"];
            int pageIndex = Convert.ToInt32(pageNumberAsString ?? 1.ToString()); 
            var result = await _productsRepository.GetProductsByCategoryAsync(name, pageIndex);

            return new OkObjectResult(new ApiResponse<PagedList<ProductSummary>>("Products received successfully", new PagedList<ProductSummary>(result.Items.Select(p => new ProductSummary
            {
                Category = p.Category,
                CreationDate = p.CreationDate,
                Id = p.Id,
                IsSold = p.IsSold,
                Name = p.Name,
                Price = p.Price,
                Cover = p.Cover,
                Likes = p.Likes,
                Views = p.Views,
                UserId = p.UserId,
            }), result.PageIndex, result.PagesCount, result.PageSize, result.TotalItemsCount)));
        }
    }
}
