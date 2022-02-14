using Vendr.Common;
using Vendr.Contrib.Wishlists.Persistence.Repositories;
using Vendr.Contrib.Wishlists.Persistence.Repositories.Implement;
using Vendr.Infrastructure;

#if NETFRAMEWORK
using Umbraco.Core.Scoping;
#else
using Umbraco.Cms.Core.Scoping;
#endif

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
            return new WishlistRepository((IDatabaseUnitOfWork)uow, _scopeAccessor.AmbientScope.SqlContext);
        }
    }
}