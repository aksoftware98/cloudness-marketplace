using CloudnessMarketplace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Interfaces
{
    public interface IPictureRepository
    {
        Task<string> AddAsync(string url, string name, long fileSize, string userId);

        Task RemoveAsync(string id);

        Task<Picture> GetByUrlAsync(string url);

        Task<IEnumerable<Picture>> ListPendingAsync(); 
    }
}
