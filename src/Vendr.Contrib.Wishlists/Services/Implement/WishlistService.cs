using System;
using System.Collections.Generic;
using System.Linq;
using Vendr.Contrib.Wishlists.Events;
using Vendr.Contrib.Wishlists.Factories;
using Vendr.Contrib.Wishlists.Models;
using Vendr.Core;
using Vendr.Core.Events;
using Vendr.Core.Models;

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
            Wishlist result;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateWishlistRepository(uow))
            {
                if (wishlist.Id == Guid.Empty)
                {
                    wishlist.Id = Guid.NewGuid();
                    wishlist.CreateDate = DateTime.UtcNow;

                    EventBus.Dispatch(new WishlistAddingNotification(wishlist));
                    uow.ScheduleNotification(new WishlistAddedNotification(wishlist));
                }

                wishlist.UpdateDate = DateTime.UtcNow;

                result = repo.SaveWishlist(wishlist);
                uow.Complete();
            }

            return result;
        }

        public void DeleteWishlist(Guid id)
        {
            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateWishlistRepository(uow))
            {
                repo.DeleteWishlist(id);
                uow.Complete();
            }
        }

        public PagedResult<Wishlist> SearchWishlists(Guid storeId, string searchTerm = null, DateTime? startDate = null, DateTime? endDate = null, long pageNumber = 1, long pageSize = 50)
        {
            PagedResult<Wishlist> results;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateWishlistRepository(uow))
            {
                results = repo.SearchWishlists(storeId,
                    searchTerm: searchTerm,
                    startDate: startDate,
                    endDate: endDate,
                    pageNumber: pageNumber,
                    pageSize: pageSize);

                uow.Complete();
            }

            return results;
        }
    }
}