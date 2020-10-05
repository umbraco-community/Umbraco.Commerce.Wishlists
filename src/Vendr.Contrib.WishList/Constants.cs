namespace Vendr.Contrib.Wishlists
{
    /// <summary>
    /// Constants all the identifiers
    /// </summary>
    public static partial class Constants
    {
        // generic constants can go here

        public static class DatabaseSchema
        {
            public const string TableNamePrefix = "vendr";

            public static class Tables
            {
                public const string Wishlist = TableNamePrefix + "WishList";
            }
        }

        public static class Trees
        {
            public static class Wishlist
            {
                /// <summary>
                /// Id for wishlist node
                /// </summary>
                public const string Id = "200";

                /// <summary>
                /// System wishlist icon
                /// </summary>
                public const string Icon = "icon-notepad";

                /// <summary>
                /// System wishlist node type
                /// </summary>
                public const string NodeType = "Wishlist";
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