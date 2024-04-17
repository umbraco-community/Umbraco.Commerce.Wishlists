using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.Common;
using Umbraco.Commerce.Common.Events;
using Umbraco.Commerce.Core.Services;
using Umbraco.Commerce.Core.Session;
using Umbraco.Commerce.Wishlists.Models;
using Vendr.Contrib.Wishlists.Configuration;
using Vendr.Contrib.Wishlists.Events;
using Vendr.Contrib.Wishlists.Persistence;

namespace Umbraco.Commerce.Wishlists.Services.Implement
{
    public class WishlistService : IWishlistService
    {
        private readonly IUnitOfWorkProvider _uowProvider;
        private readonly IWishlistRepositoryFactory _repositoryFactory;
        private readonly ISessionManager _sessionManager;
        private readonly IStoreService _storeService;
        private readonly IOrderService _orderService;
        private readonly IOrderStatusService _orderStatusService;
        private readonly ICountryService _countryService;
        private readonly UmbracoCommerceWishlistsSettings _settings;

        public WishlistService(
            IUnitOfWorkProvider uowProvider,
            IWishlistRepositoryFactory repositoryFactory,
            ISessionManager sessionManager,
            IStoreService storeService,
            IOrderService orderService,
            IOrderStatusService orderStatusService,
            ICountryService countryService,
            UmbracoCommerceWishlistsSettings settings)
        {
            _uowProvider = uowProvider;
            _repositoryFactory = repositoryFactory;
            _sessionManager = sessionManager;
            _storeService = storeService;
            _orderService = orderService;
            _orderStatusService = orderStatusService;
            _countryService = countryService;
            _settings = settings;
        }

        public void AddProduct(Guid storeId, Guid wishlistId, string productReference, decimal qty = 1)
        {
            var store = _storeService.GetStore(storeId);

            var orderStatus = _orderStatusService.GetOrderStatus(store.DefaultOrderStatusId.Value);
            var taxClass = _sessionManager.GetDefaultTaxClass(storeId); 
            var currency = _sessionManager.GetDefaultCurrency(storeId);
            var country = _sessionManager.GetDefaultPaymentCountry(storeId);

            string languageIsoCode = country?.Code;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateWishlistRepository(uow))
            {
                // TODO: How to create new wishlist - create wishlist or order first?

                // Get wishlist
                var wishlist = repo.GetWishlist(wishlistId) ?? repo.CreateWishlist(storeId, Guid.NewGuid(), _settings.WishtListName);

                // Get reference order or create new
                var order = wishlist.OrderId != null ? _orderService.GetOrder(wishlist.OrderId)?.AsWritable(uow) : null 
                    ?? Order.Create(uow, storeId, languageIsoCode, currency.Id, taxClass.Id, orderStatus.Id);

                wishlist.OrderId = order.Id;
                repo.SaveWishlist(wishlist);

                order.AddProduct(productReference, qty);

                _orderService.SaveOrder(order);

                uow.Complete();
            }
        }

        public void AddProduct(string productReference, decimal qty, IDictionary<string, string> properties)
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

        public PagedResult<Wishlist> GetWishlistsForCustomer(Guid storeId, string customerReference, long pageNumber = 1, long pageSize = 50)
        {
            PagedResult<Wishlist> results;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateWishlistRepository(uow))
            {
                results = repo.SearchWishlists(storeId,
                    customerReferences: new[] { customerReference },
                    pageNumber: pageNumber,
                    pageSize: pageSize);

                uow.Complete();
            }

            return results;
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