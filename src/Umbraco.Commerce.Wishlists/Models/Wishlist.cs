namespace Umbraco.Commerce.Wishlists.Models
{
    public class Wishlist
    {
        public Guid Id { get; internal set; }

        public Guid StoreId { get; internal set; }

        public Guid OrderId { get; internal set; }

        public string? CustomerReference { get; internal set; }

        public string? Title { get; set; }

        public string? Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Wishlist(Guid storeId, Guid orderId)
            : this(Guid.Empty, storeId, orderId, null)
        { }

        public Wishlist(Guid storeId, Guid orderId, string? customerReference)
            : this(Guid.Empty, storeId, orderId, customerReference)
        { }

        public Wishlist(Guid id, Guid storeId, Guid orderId, string? customerReference)
        {
            Id = id;
            StoreId = storeId;
            OrderId = orderId;
            CustomerReference = customerReference;
        }
    }
}