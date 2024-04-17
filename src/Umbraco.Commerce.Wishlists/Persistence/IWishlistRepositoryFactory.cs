using Umbraco.Commerce.Common;
using Umbraco.Commerce.Wishlists.Persistence.Repositories;

namespace Umbraco.Commerce.Wishlists.Persistence
{
    public interface IWishlistRepositoryFactory
    {
        IWishlistRepository CreateWishlistRepository(IUnitOfWork uow);
    }
}
