using System;
using Vendr.Contrib.Wishlists.Services;

namespace Vendr.Contrib.Wishlists.Api
{
    public class VendrWishlistsApi
    {
        public static VendrWishlistsApi Instance { get; internal set; }

        private Lazy<IWishlistService> _wishlistService;
        public IWishlistService WishlistService => _wishlistService.Value;

        public VendrWishlistsApi(Lazy<IWishlistService> wishlistService)
        {
            _wishlistService = wishlistService;
        }
    }
}