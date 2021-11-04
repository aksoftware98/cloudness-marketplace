using System.Threading.Tasks;

namespace CloudnessMarketplace.Data.Interfaces
{
    public interface IProductLikesRepository
    {
        Task LikeProductAsync(string productId, string userId);

        Task RemoveLikeAsync(string productId, string userId);
    }
}
