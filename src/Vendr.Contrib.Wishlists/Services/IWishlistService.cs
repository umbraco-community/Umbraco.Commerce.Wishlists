using System;
using System.Collections.Generic;
using Vendr.Contrib.Wishlists.Models;

namespace Vendr.Contrib.Wishlists.Services
{
    public interface IWishlistService
    {
        /// <summary>
        /// Gets a wishlist.
        /// </summary>
        Wishlist GetWishlist(Guid id);

        /// <summary>
        /// Gets wishlists.
        /// </summary>
        IEnumerable<Wishlist> GetWishlists(Guid[] ids);

        /// <summary>
        /// Gets wishlists for customer.
        /// </summary>
        IEnumerable<Wishlist> GetWishlistsForCustomer(Guid storeId, string customerReference, long currentPage, long itemsPerPage, out long totalRecords);

        /// <summary>
        /// Gets paged result of wishlists.
        /// </summary>
        IEnumerable<Wishlist> GetPagedResults(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords);

        /// <summary>
        /// Search wishlist.
        /// </summary>
        IEnumerable<Wishlist> SearchWishlists(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords, string searchTerm = "", DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Add product to wishlist.
        /// </summary>
        void AddProduct(string productReference, decimal qty);

        /// <summary>
        /// Add product to wishlist.
        /// </summary>
        void AddProduct(string productReference, decimal qty, IDictionary<string, string> properties);

        /// <summary>
        /// Save wishlist.
        /// </summary>
        Wishlist SaveWishlist(Wishlist review);

        /// <summary>
        /// Delete wishlist.
        /// </summary>
        void DeleteWishlist(Guid id);
    }
}
