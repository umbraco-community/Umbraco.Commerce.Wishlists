using System;
using System.Collections.Generic;
using Umbraco.Core.Persistence.Querying;
using Vendr.Contrib.Wishlists.Models;
using Vendr.Core.Models;

namespace Vendr.Contrib.Wishlists.Persistence.Repositories
{
    public interface IWishlistRepository : IDisposable
    {
        Wishlist GetWishlist(Guid id);

        IEnumerable<Wishlist> GetWishlists(Guid[] ids);
        
        PagedResult<Wishlist> SearchWishlists(Guid storeId, string searchTerm = null, string[] customerReferences = null, DateTime? startDate = null, DateTime? endDate = null, long pageNumber = 1, long pageSize = 50);

        Wishlist SaveWishlist(Wishlist wishlist);

        void DeleteWishlist(Guid id);
    }
}