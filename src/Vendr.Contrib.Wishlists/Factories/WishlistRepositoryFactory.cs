using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using Vendr.Contrib.Wishlists.Persistence.Repositories;
using Vendr.Contrib.Wishlists.Persistence.Repositories.Implement;
using Vendr.Core;

namespace Vendr.Contrib.Wishlists.Factories
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
            return new WishlistRepository((IDatabaseUnitOfWork)uow, _scopeAccessor.AmbientScope.SqlContext);
        }
    }
}