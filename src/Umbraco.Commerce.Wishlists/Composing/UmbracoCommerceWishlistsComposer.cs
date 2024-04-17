using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Umbraco.Commerce.Wishlists.Composing
{
    public class UmbracoCommerceWishlistsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddUmbracoCommerceWishlists();
        }
    }
}
