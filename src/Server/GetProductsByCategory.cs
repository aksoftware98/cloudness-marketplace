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

            return new OkObjectResult(new ApiResponse<PagedList<ProductDto>>("Products received successfully", new PagedList<ProductDto>(result.Items.Select(p => new ProductDto
            {
                Category = p.Category,
                CreationDate = p.CreationDate,
                Description = p.Description,
                Id = p.Id,
                IsSold = p.IsSold,
                Metadata = p.Metadata,
                Name = p.Name,
                PictureUrls = p.PictureUrls,
                Price = p.Price,
                SellingDate = p.SellingDate,
                UserId = p.UserId,
            }), result.PageIndex, result.PagesCount, result.PageSize, result.TotalItemsCount)));
        }
    }
}
