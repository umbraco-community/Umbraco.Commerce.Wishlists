using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Commerce.Common;
using Umbraco.Commerce.Wishlists.Persistence;
using Umbraco.Commerce.Wishlists.Persistence.Repositories;
using Vendr.Contrib.Wishlists.Persistence.Repositories.Implement;

namespace Vendr.Contrib.Wishlists.Persistence
{
    public class WishlistRepositoryFactory : IWishlistRepositoryFactory
    {
        private readonly IScopeAccessor _scopeAccessor;

        public WishlistRepositoryFactory(IScopeAccessor scopeAccessor)
        {
            _scopeAccessor = scopeAccessor;
        }

        public IWishlistRepository CreateWishlistRepository(IUnitOfWork uow)
        {
            return new WishlistRepository((IDatabaseUnitOfWork)uow, _scopeAccessor?.AmbientScope?.SqlContext);
        }
    }
}