using CloudnessMarketplace.Shared.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Shared.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {

        public ProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(p => p.Description)
                .MinimumLength(10)
                .WithMessage("Description must be at least 10 characters");

            RuleFor(p => p.Category)
                .NotEmpty()
                .WithMessage("Category is required");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Price cannot be 0 or negative value");

            RuleFor(p => p.PictureUrls)
                .NotEmpty()
                .WithMessage("At least one picture must be chosen for this product"); 

        }

    }
}
