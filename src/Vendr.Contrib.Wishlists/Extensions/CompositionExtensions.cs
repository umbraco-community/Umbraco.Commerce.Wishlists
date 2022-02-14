using Vendr.Contrib.Wishlists.Events;
//using Vendr.Contrib.Wishlists.Events.Handlers;
using Vendr.Extensions;
using Vendr.Umbraco.Web.Events.Notification;

#if NETFRAMEWORK
using IBuilder = Umbraco.Core.Composing.Composition;
#else
using Vendr.Contrib.Wishlists.Notifications;
using IBuilder = Umbraco.Cms.Core.DependencyInjection.IUmbracoBuilder;
#endif

namespace Vendr.Contrib.Wishlists.Extensions
{
    internal static class CompositionExtensions
    {
        public static IBuilder AddVendrWishlistsEventHandlers(this IBuilder builder)
        {
            //builder.WithNotificationEvent<WishlistAddedNotification>()
            //    .RegisterHandler<LogWishlistAddedActivity>();

            return builder;
        }
    }
}
