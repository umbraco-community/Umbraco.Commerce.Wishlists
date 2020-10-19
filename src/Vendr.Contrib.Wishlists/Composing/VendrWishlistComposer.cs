using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.Web.Composing;

namespace Vendr.Contrib.Wishlists.Composing
{
    //[RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    [ComposeAfter(typeof(VendrWebComposer))]
    public class VendrWishlistComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            //composition.RegisterUnique<IWishlistRepositoryFactory, WishlistRepositoryFactory>();

            // Register services
            //composition.Register<IWishlistService, WishlistService>();

            // Register events


            // Register component
            composition.Components()
                .Append<VendrWishlistComponent>();
        }
    }
}