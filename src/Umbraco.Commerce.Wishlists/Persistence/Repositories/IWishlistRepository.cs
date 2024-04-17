using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.Wishlists.Models;

namespace Umbraco.Commerce.Wishlists.Persistence.Repositories
{
    public interface IWishlistRepository : IDisposable
    {
        Wishlist GetWishlist(Guid id);

        IEnumerable<Wishlist> GetWishlists(Guid[] ids);
        
        PagedResult<Wishlist> SearchWishlists(Guid storeId, string? searchTerm = null, string[]? customerReferences = null, DateTime? startDate = null, DateTime? endDate = null, long pageNumber = 1, long pageSize = 50);
        
        Wishlist CreateWishlist(Guid storeId, Guid? orderId = null, string? name = null);

        Wishlist SaveWishlist(Wishlist wishlist);

        void DeleteWishlist(Guid id);
    }
}