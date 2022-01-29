using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data
{
    public static class DependencyInjectionExtensions
    {
        
        public static void AddDataRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IPictureRepository, PicturesRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductLikesRepository, ProductLikesRepository>();
            services.AddScoped<IDatabaseProvider, CosmosDbDataProvider>();
        }

    }
}
