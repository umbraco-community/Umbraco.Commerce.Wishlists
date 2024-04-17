using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Commerce.Wishlists.Models;
using Umbraco.Commerce.Wishlists.Services;

namespace Umbraco.Commerce.Wishlists.Web.Controllers
{
    [PluginController(Constants.Internals.PluginControllerName)]
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