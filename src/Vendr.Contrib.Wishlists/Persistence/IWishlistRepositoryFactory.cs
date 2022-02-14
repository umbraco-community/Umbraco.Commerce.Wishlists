using Vendr.Common;
using Vendr.Contrib.Wishlists.Persistence.Repositories;

namespace Vendr.Contrib.Wishlists.Persistence
{
    public interface IWishlistRepositoryFactory
    {
        IWishlistRepository CreateWishlistRepository(IUnitOfWork uow);
    }
}
