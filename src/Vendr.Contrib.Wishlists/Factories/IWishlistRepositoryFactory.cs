using Vendr.Contrib.Wishlists.Persistence.Repositories;
using Vendr.Core;

namespace Vendr.Contrib.Wishlists.Factories
{
    public interface IWishlistRepositoryFactory
    {
        IWishlistRepository CreateWishlistRepository(IUnitOfWork uow);
    }
}
