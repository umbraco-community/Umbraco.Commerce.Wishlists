using System;
using System.Collections.Generic;
using Umbraco.Core.Persistence.Querying;
using Vendr.Contrib.Wishlists.Models;

namespace Vendr.Contrib.Wishlists.Persistence.Repositories
{
    public interface IWishlistRepository : IDisposable
    {
        Wishlist Get(Guid id);

        IEnumerable<Wishlist> Get(Guid[] ids);

        IEnumerable<Wishlist> GetMany(Guid storeId, string productReference, long pageIndex, long pageSize, out long totalRecords);

        IEnumerable<Wishlist> GetForCustomer(Guid storeId, string customerReference, long pageIndex, long pageSize, out long totalRecords, string productReference = null);

        IEnumerable<Wishlist> GetPagedReviewsByQuery(Guid storeId, IQuery<Wishlist> query, long pageIndex, long pageSize, out long totalRecords);

        IEnumerable<Wishlist> SearchWishLists(Guid storeId, long pageIndex, long pageSize, out long totalRecords, string[] statuses, decimal[] ratings, string searchTerm = "", DateTime? startDate = null, DateTime? endDate = null);

        Wishlist Save(Wishlist wishlist);

        Wishlist Insert(Wishlist wishlist);

        void Delete(Guid id);
    }
}