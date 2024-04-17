namespace Umbraco.Commerce.Wishlists.Extensions
{
    internal static class CompositionExtensions
    {
        public static IUmbracoBuilder AddVendrWishlistsEventHandlers(this IUmbracoBuilder builder)
        {
            //builder.WithNotificationEvent<WishlistAddedNotification>()
            //    .RegisterHandler<LogWishlistAddedActivity>();

            return builder;
        }
    }
}
