using System;
using System.Collections.Generic;
using System.Linq;
using Vendr.Contrib.Wishlists.Factories;
using Vendr.Contrib.Wishlists.Models;
using Vendr.Core;

namespace Vendr.Contrib.Wishlists.Services.Implement
{
    public class WishlistService : IWishlistService
    {
        private readonly IUnitOfWorkProvider _uowProvider;
        private readonly IWishlistRepositoryFactory _repositoryFactory;

        public WishlistService(IUnitOfWorkProvider uowProvider, IWishlistRepositoryFactory repositoryFactory)
        {
            _uowProvider = uowProvider;
            _repositoryFactory = repositoryFactory;
        }

        public void AddProduct(string productReference, decimal qty)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(string productReference, decimal qty, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }

        public void DeleteWishlist(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wishlist> GetPagedResults(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords)
        {
            throw new NotImplementedException();
        }

        public Wishlist GetWishlist(Guid id)
        {
            Wishlist wishlist;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateWishlistRepository(uow))
            {
                wishlist = repo.GetWishlist(id);
                uow.Complete();
            }

            return wishlist;
        }

        public IEnumerable<Wishlist> GetWishlists(Guid[] ids)
        {
            List<Wishlist> wishlists = new List<Wishlist>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateWishlistRepository(uow))
            {
                var lists = repo.GetWishlists(ids);
                wishlists.AddRange(lists);
                uow.Complete();
            }

            return wishlists;
        }

        public IEnumerable<Wishlist> GetWishlistsForCustomer(Guid storeId, string customerReference, long currentPage, long itemsPerPage, out long totalRecords)
        {
            throw new NotImplementedException();
        }

        public Wishlist SaveWishlist(Wishlist wishlist)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wishlist> SearchWishlists(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords, string searchTerm = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }
    }
}