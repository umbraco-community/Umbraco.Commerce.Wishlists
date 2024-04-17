using Umbraco.Commerce.Wishlists.Services;

namespace Umbraco.Commerce.Wishlists.Api
{
    public class UmbracoCommerceWishlistsApi
    {
        public static UmbracoCommerceWishlistsApi Instance { get; internal set; }

        private Lazy<IWishlistService> _wishlistService;

        public IWishlistService WishlistService => _wishlistService.Value;

        public UmbracoCommerceWishlistsApi(Lazy<IWishlistService> wishlistService)
        {
            _wishlistService = wishlistService;
        }
    }
}