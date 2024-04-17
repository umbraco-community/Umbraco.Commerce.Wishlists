using IBuilder = Umbraco.Cms.Core.DependencyInjection.IUmbracoBuilder;

namespace Umbraco.Commerce.Wishlists.Extensions
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
