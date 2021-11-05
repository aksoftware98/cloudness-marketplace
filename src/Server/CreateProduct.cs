using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudnessMarketplace.Shared.Models;
using CloudnessMarketplace.Shared.Responses;
using CloudnessMarketplace.Data.Interfaces;
using FluentValidation;
using System.Linq;

namespace CloudnessMarketplace.Functions
{
    public class CreateProduct
    {

        private readonly IProductsRepository _productsRepository;
        private readonly IValidator<ProductDto> _productValidator; 
        
        public CreateProduct(IProductsRepository productsRepository, IValidator<ProductDto> productValidator)
        {
            _productsRepository = productsRepository;
            _productValidator = productValidator;
        }

        [FunctionName("CreateProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create a new product");

            // TODO: Get the user id from the logged in user 
            string userId = "Test";
            ProductDto model = null;

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                model = JsonConvert.DeserializeObject<ProductDto>(requestBody);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ApiErrorResponse("Failed to create a new product", new[]
                {
                    "Invalid product object"
                }));
            }

            // Validate the view 
            var validationResult = _productValidator.Validate(model);
            if (!validationResult.IsValid)
                return new BadRequestObjectResult(new ApiErrorResponse("Failed to create a new product", validationResult.Errors.Select(e => e.ErrorMessage)));

            model.Id = Guid.NewGuid().ToString();
            var product = await _productsRepository.CreateAsync(new Models.Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Category = model.Category,
                CreationDate = DateTime.UtcNow,
                IsSold = false,
                Metadata = model.Metadata,
                PictureUrls = model.PictureUrls,
                Price = model.Price,
                UserId = userId
            });

            return new OkObjectResult(new ApiResponse<ProductDto>("Product has been created successfully", model));
        }
    }
}
