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
using System.Linq;
using CloudnessMarketplace.Shared.Models;
using CloudnessMarketplace.Shared.Responses;
using System.Collections;
using System.Collections.Generic;

namespace CloudnessMarketplace.Functions
{
    public class GetCategories
    {

        private readonly ICategoriesRepository _categoriesRepo;

        public GetCategories(ICategoriesRepository categoriesRepo)
        {
            _categoriesRepo = categoriesRepo;
        }

        [FunctionName("GetCategories")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get categories request");

            var categories = await _categoriesRepo.GetCategoriesAsync();
            var data = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IconUrl = c.IconUrl
            });

            return new OkObjectResult(new ApiResponse<IEnumerable<CategoryDto>>("Categories retrieved successfully", data));
        }
    }
}
