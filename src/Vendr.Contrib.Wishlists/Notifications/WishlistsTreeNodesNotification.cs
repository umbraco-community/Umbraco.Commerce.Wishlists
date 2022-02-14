#if NET
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.BackOffice.Trees;
using Vendr.Umbraco.Web.Trees;

namespace Vendr.Contrib.Wishlists.Notifications
{
    public class WishlistsTreeNodesNotification : INotificationHandler<TreeNodesRenderingNotification>
    {
        private readonly UmbracoApiControllerTypeCollection _apiControllers;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        public WishlistsTreeNodesNotification(
            UmbracoApiControllerTypeCollection apiControllers,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _apiControllers = apiControllers;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }

        public TreeNode CreateTreeNode(string id, string parentId, FormCollection queryStrings, string title, string icon, bool hasChildren, string routePath)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

            var controllerActionDescriptor =
                _actionContextAccessor.ActionContext.ActionDescriptor as ControllerActionDescriptor;

            var jsonUrl = urlHelper.GetTreeUrl(_apiControllers, controllerActionDescriptor.ControllerTypeInfo, id, queryStrings);
            var menuUrl = urlHelper.GetMenuUrl(_apiControllers, controllerActionDescriptor.ControllerTypeInfo, id, queryStrings);

            var treeNode = new TreeNode(id, parentId, jsonUrl, menuUrl)
            { 
                Name = title,
                RoutePath = routePath,
                Icon = icon,
                HasChildren = hasChildren
            };

            return treeNode;
        }

        public void Handle(TreeNodesRenderingNotification notification)
        {
            if (notification.TreeAlias == Vendr.Core.Constants.System.ProductAlias
                && notification.QueryString["nodeType"] == Vendr.Core.Constants.Entities.EntityTypes.Store)
            {
                var mainRoute = Vendr.Umbraco.Constants.Sections.Commerce + "/" + Constants.System.ProductAlias;

                var storeId = notification.QueryString["id"];
                var id = Constants.Trees.Wishlists.Id;
                
                var wishlistsNode = CreateTreeNode(id, storeId, notification.QueryString, "Wishlists", Constants.Trees.Wishlists.Icon, false, $"{mainRoute}/wishlist-list/{storeId}");

                wishlistsNode.Path = $"-1,{storeId},{id}";
                wishlistsNode.NodeType = Constants.Trees.Wishlists.NodeType;

                wishlistsNode.AdditionalData.Add("storeId", storeId);
                wishlistsNode.AdditionalData.Add("tree", Vendr.Umbraco.Constants.Trees.Stores.Alias);
                wishlistsNode.AdditionalData.Add("application", Vendr.Umbraco.Constants.Sections.Commerce);

                var optNodeIndex = notification.Nodes.FindIndex(x => x.NodeType == "Options");
                var index = optNodeIndex >= 0 ? optNodeIndex : notification.Nodes.Count;

                notification.Nodes.Insert(index, wishlistsNode);
            }
        }
    }
}

#endif