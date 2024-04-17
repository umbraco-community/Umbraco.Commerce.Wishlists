namespace Umbraco.Commerce.Wishlists
{
    /// <summary>
    /// Constants all the identifiers
    /// </summary>
    public static partial class Constants
    {
        internal static partial class Internals
        {
            public const string PluginControllerName = "UmbracoCommerceWishlists";
        }

        public static class System
        {
            public const string ProductAlias = "umbracoCommerceWishlists";

            public const string ProductName = "UmbracoCommerceWishlists";

            public const string MigrationPlanName = "Umbraco.Commerce.Wishlists";
        }

        public static class DatabaseSchema
        {
            public const string TableNamePrefix = "umbracoCommerce";

            public static class Tables
            {
                public const string Wishlist = TableNamePrefix + "Wishlist";
            }
        }

        public static class Trees
        {
            public static class Wishlists
            {
                /// <summary>
                /// Alias for wishlists node
                /// </summary>
                public const string Alias = "wishlist";

                /// <summary>
                /// Id for wishlists node
                /// </summary>
                public const string Id = "200";

                /// <summary>
                /// System wishlists icon
                /// </summary>
                public const string Icon = "icon-rate";

                /// <summary>
                /// System wishlists node type
                /// </summary>
                public const string NodeType = "Wishlis";
            }
        }

        public static class Entities
        {
            public static class EntityTypes
            {
                /// <summary>
                /// Wishlist entity type
                /// </summary>
                public const string Wishlist = "Wishlist";
            }
        }
    }
}
