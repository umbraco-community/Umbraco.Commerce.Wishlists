using System;
using System.Collections.Generic;
using Vendr.Common.Models;
using Vendr.Contrib.Wishlists.Models;

namespace Vendr.Contrib.Wishlists.Persistence.Repositories
{
    public interface IWishlistRepository : IDisposable
    {
        Wishlist GetWishlist(Guid id);

        IEnumerable<Wishlist> GetWishlists(Guid[] ids);
        
        PagedResult<Wishlist> SearchWishlists(Guid storeId, string searchTerm = null, string[] customerReferences = null, DateTime? startDate = null, DateTime? endDate = null, long pageNumber = 1, long pageSize = 50);
        
        Wishlist CreateWishlist(string name);

        Wishlist SaveWishlist(Wishlist wishlist);

        void DeleteWishlist(Guid id);
    }
}