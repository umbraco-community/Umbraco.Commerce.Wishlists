using Umbraco.Cms.Core.Persistence;

namespace Umbraco.Commerce.Wishlists.Persistence.Repositories
{
    internal abstract class RepositoryBase : IRepository
    {
        public virtual void Dispose()
        {
            // Dispose of any resources
        }
    }
}