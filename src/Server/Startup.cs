using CloudnessMarketplace.Data.Interfaces;
using CloudnessMarketplace.Data.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(CloudnessMarketplace.Functions.Startup))]
namespace CloudnessMarketplace.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        }
    }
}
