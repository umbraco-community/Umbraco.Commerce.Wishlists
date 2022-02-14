using System;
using System.Collections.Generic;
using Vendr.Contrib.Wishlists.Models;
using Vendr.Contrib.Wishlists.Services;

#if NETFRAMEWORK
using System.Web.Http;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Models.ContentEditing;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using Notification = Umbraco.Web.Models.ContentEditing.Notification;
#else
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Notification = Umbraco.Cms.Core.Models.ContentEditing.BackOfficeNotification;
#endif

namespace Vendr.Contrib.Wishlists.Web.Controllers
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