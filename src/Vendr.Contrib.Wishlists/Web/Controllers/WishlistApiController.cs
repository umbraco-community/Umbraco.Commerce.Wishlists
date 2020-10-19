using System;
using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using Vendr.Contrib.Wishlists.Models;
using Vendr.Contrib.Wishlists.Services;

namespace Vendr.Contrib.Wishlists.Web.Controllers
{
    [PluginController("VendrWishlists")]
    public class WishlistwApiController : UmbracoAuthorizedApiController
    {
        private readonly IWishlistService _wishlistService;

        public WishlistwApiController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public Wishlist GetWishlist(Guid id)
        {
            return _wishlistService.GetWishlist(id);
        }

        [HttpGet]
        public IEnumerable<Wishlist> GetWishlists(Guid[] ids)
        {
            return _wishlistService.GetWishlists(ids);
        }
    }
}