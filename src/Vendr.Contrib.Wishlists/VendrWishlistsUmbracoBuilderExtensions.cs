#if NET

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Vendr.Contrib.Wishlists.Api;
using Vendr.Contrib.Wishlists.Composing;
using Vendr.Contrib.Wishlists.Configuration;
using Vendr.Contrib.Wishlists.Events;
using Vendr.Contrib.Wishlists.Extensions;
using Vendr.Contrib.Wishlists.Notifications;
using Vendr.Contrib.Wishlists.Persistence;
using Vendr.Contrib.Wishlists.Services;
using Vendr.Contrib.Wishlists.Services.Implement;

namespace Vendr.Contrib.Wishlists
{
    // ================================================================
    // IMPORTANT! Whatever you change here, be sure to also update the
    // v8 equivilent in /Composing/VendrWishlistComposer.cs
    // ================================================================

    public static class VendrWishlistsUmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddVendrWishlists(this IUmbracoBuilder builder, Action<VendrWishlistsSettings> defaultOptions = default)
        {
            // Register configuration
            var options = builder.Services.AddOptions<VendrWishlistsSettings>()
                .Bind(builder.Config.GetSection(Constants.System.ProductName));

            if (defaultOptions != default)
                options.Configure(defaultOptions);

            options.ValidateDataAnnotations();

            // Register services
            builder.Services.AddTransient<IWishlistRepositoryFactory, WishlistRepositoryFactory>();
            builder.Services.AddSingleton<IWishlistService, WishlistService>();
            builder.Services.AddSingleton<VendrWishlistsApi>();

            // Register event handlers
            builder.AddNotificationHandler<TreeNodesRenderingNotification, WishlistsTreeNodesNotification>();

            // Register event handlers
            builder.AddVendrWishlistsEventHandlers();

            // Register component
            builder.Components()
                .Append<VendrWishlistsComponent>();

            return builder;
        }
    }
}

#endif