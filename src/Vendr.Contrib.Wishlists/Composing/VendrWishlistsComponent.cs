using Vendr.Contrib.Wishlists.Api;

#if NETFRAMEWORK
using Umbraco.Core.Composing;
using Umbraco.Web.Trees;
#else
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Web.BackOffice.Trees;
#endif


namespace Vendr.Contrib.Wishlists.Composing
{
    public class VendrWishlistsComponent : IComponent
    {
        private readonly VendrWishlistsApi _api;

        public VendrWishlistsComponent(VendrWishlistsApi api)
        {
            _api = api;
        }

        public void Initialize()
        {
            VendrWishlistsApi.Instance = _api;

#if NETFRAMEWORK
            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
#endif
        }

        public void Terminate()
        {
#if NETFRAMEWORK
            TreeControllerBase.TreeNodesRendering -= TreeControllerBase_TreeNodesRendering;
#endif
        }

#if NETFRAMEWORK
        void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            if (sender.TreeAlias == Vendr.Core.Constants.System.ProductAlias && e.QueryStrings["nodeType"] == Vendr.Core.Constants.Entities.EntityTypes.Store)
            {
                var mainRoute = Vendr.Umbraco.Constants.Sections.Commerce + "/" + Constants.System.ProductAlias;

                var storeId = e.QueryStrings["id"];
                var id = Constants.Trees.Wishlists.Id;

                var reviewsNode = sender.CreateTreeNode(id, storeId, e.QueryStrings, "Reviews", Constants.Trees.Wishlists.Icon, false, $"{mainRoute}/wishlist-list/{storeId}");

                reviewsNode.Path = $"-1,{storeId},{id}";
                reviewsNode.NodeType = Constants.Trees.Wishlists.NodeType;

                reviewsNode.AdditionalData.Add("storeId", storeId);
                reviewsNode.AdditionalData.Add("tree", Vendr.Umbraco.Constants.Trees.Stores.Alias);
                reviewsNode.AdditionalData.Add("application", Vendr.Umbraco.Constants.Sections.Commerce);

                var optNodeIndex = e.Nodes.FindIndex(x => x.NodeType == "Options");
                var index = optNodeIndex >= 0 ? optNodeIndex : e.Nodes.Count;

                e.Nodes.Insert(index, reviewsNode);
            }
        }
#endif
    }
}