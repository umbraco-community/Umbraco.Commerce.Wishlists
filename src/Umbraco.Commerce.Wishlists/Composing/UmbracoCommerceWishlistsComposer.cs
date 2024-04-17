using Umbraco.Cms.Core.Composing;
using IBuilder = Umbraco.Cms.Core.DependencyInjection.IUmbracoBuilder;

namespace Umbraco.Commerce.Wishlists.Composing
{
    public class UmbracoCommerceWishlistsComposer : IComposer
    {
        public void Compose(IBuilder builder)
        {
            builder.AddUmbracoCommerceWishlists();
        }
    }
}