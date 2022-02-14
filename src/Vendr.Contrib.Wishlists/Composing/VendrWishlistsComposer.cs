using Vendr.Contrib.Wishlists.Api;
using Vendr.Contrib.Wishlists.Configuration;
using Vendr.Contrib.Wishlists.Extensions;
using Vendr.Contrib.Wishlists.Persistence;
using Vendr.Contrib.Wishlists.Services.Implement;
using Vendr.Contrib.Wishlists.Services;

#if NETFRAMEWORK
using Umbraco.Core;
using Umbraco.Core.Composing;
using IBuilder = Umbraco.Core.Composing.Composition;
#else
using Umbraco.Cms.Core.Composing;
using IBuilder = Umbraco.Cms.Core.DependencyInjection.IUmbracoBuilder;
#endif

namespace Vendr.Contrib.Wishlists.Composing
{
    // ================================================================
    // IMPORTANT! Whatever you change here, be sure to also update the
    // v9 equivilent in /VendrWishlistsUmbracoBuilderExtensions.cs
    // ================================================================

    public class VendrWishlistsComposer : IUserComposer
    {
        public void Compose(IBuilder builder)
        {
#if NETFRAMEWORK

            // Register settings
            builder.Register<VendrWishlistsSettings>(Lifetime.Singleton);

            // Register API
            builder.Register<VendrWishlistsApi>(Lifetime.Singleton);

            // Register factories
            builder.RegisterUnique<IWishlistRepositoryFactory, WishlistRepositoryFactory>();

            // Register services
            builder.Register<IWishlistService, WishlistService>(Lifetime.Singleton);

            // Register event handlers
            builder.AddVendrReviewsEventHandlers();

            // Register component
            builder.Components()
                .Append<VendrWishlistsComponent>();
#else
        // If Vendr Wishlists hasn't been added manually by now, 
        // add it automatically with the default configuration.
        // If Vendr Reviews has already been added manully, then 
        // the AddVendrWishlists() call will just exit early.
        builder.AddVendrWishlists();
#endif
        }
    }
}