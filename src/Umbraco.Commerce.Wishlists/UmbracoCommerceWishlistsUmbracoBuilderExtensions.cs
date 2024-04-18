#if NET

using Umbraco.Commerce.Wishlists.Api;
using Umbraco.Commerce.Wishlists.Composing;
using Umbraco.Commerce.Wishlists.Configuration;
using Umbraco.Commerce.Wishlists.Notifications;
using Umbraco.Commerce.Wishlists.Persistence;
using Umbraco.Commerce.Wishlists.Services;
using Umbraco.Commerce.Wishlists.Services.Implement;

namespace Umbraco.Commerce.Wishlists
{
    public static class UmbracoCommerceWishlistsUmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddUmbracoCommerceeWishlists(this IUmbracoBuilder builder, Action<UmbracoCommerceWishlistsSettings>? defaultOptions = default)
        {
            // Register configuration
            var options = builder.Services.AddOptions<UmbracoCommerceWishlistsSettings>()
                .Bind(builder.Config.GetSection("Umbraco:Commerce:Wishlists"));

            options.ValidateDataAnnotations();

            // Register services
            builder.Services.AddTransient<IWishlistRepositoryFactory, WishlistRepositoryFactory>();
            builder.Services.AddSingleton<IWishlistService, WishlistService>();
            builder.Services.AddSingleton<UmbracoCommerceWishlistsApi>();

            // Register event handlers
            builder.AddNotificationHandler<TreeNodesRenderingNotification, WishlistsTreeNodesNotification>();

            // Register event handlers
            builder.AddUmbracoCommerceWishlistsEventHandlers();

            // Register component
            builder.Components()
                .Append<UmbracoCommerceWishlistsComponent>();

            return builder;
        }
    }
}

#endif
