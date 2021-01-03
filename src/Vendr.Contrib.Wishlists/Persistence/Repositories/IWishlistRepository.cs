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

        IEnumerable<Wishlist> GetMany(Guid storeId, string productReference, long pageIndex, long pageSize, out long totalRecords);

        IEnumerable<Wishlist> GetForCustomer(Guid storeId, string customerReference, long pageIndex, long pageSize, out long totalRecords);

        IEnumerable<Wishlist> GetPagedWishlistsByQuery(Guid storeId, IQuery<Wishlist> query, long pageIndex, long pageSize, out long totalRecords);

        PagedResult<Wishlist> SearchWishlists(Guid storeId, string searchTerm = null, string[] customerReferences = null, DateTime? startDate = null, DateTime? endDate = null, long pageNumber = 1, long pageSize = 50);

        Wishlist Save(Wishlist wishlist);

        Wishlist Insert(Wishlist wishlist);

        void Delete(Guid id);
    }
}