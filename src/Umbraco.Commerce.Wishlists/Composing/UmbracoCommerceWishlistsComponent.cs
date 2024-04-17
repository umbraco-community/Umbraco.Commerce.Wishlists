using Umbraco.Cms.Core.Composing;
using Umbraco.Commerce.Wishlists.Api;

namespace Umbraco.Commerce.Wishlists.Composing
{
    public class UmbracoCommerceWishlistsComponent : IComponent
    {
        private readonly UmbracoCommerceWishlistsApi _api;

        public UmbracoCommerceWishlistsComponent(UmbracoCommerceWishlistsApi api)
        {
            _api = api;
        }

        public void Initialize()
        {
            UmbracoCommerceWishlistsApi.Instance = _api;
        }

        public void Terminate()
        {
        }
    }
}