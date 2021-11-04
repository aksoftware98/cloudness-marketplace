using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<string> GetCategoriesAsync();
    }
}
