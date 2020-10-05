using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Umbraco.Web.Models.ContentEditing;

namespace Vendr.Contrib.Wishlists.Models
{
    [DataContract(Name = "wishlist", Namespace = "")]
    public class Wishlist
    {
        public Wishlist() { }

        public Wishlist(Guid id)
        {
            Id = id;
            Icon = "icon-notepad";
            Notifications = new List<Notification>();
        }

        [DataMember(Name = "path")]
        public string[] Path => new string[] { "-1", StoreId.ToString(), Constants.Trees.Wishlist.Id, Id.ToString() };

        [DataMember(Name = "id")]
        public Guid Id { get; internal set; }

        [DataMember(Name = "storeId")]
        public Guid StoreId { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; internal set; }

        [DataMember(Name = "createDate")]
        public DateTime CreateDate { get; set; }

        [DataMember(Name = "updateDate")]
        public DateTime UpdateDate { get; set; }

        [DataMember(Name = "customerReference")]
        public string CustomerReference { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "notifications")]
        [ReadOnly(true)]
        public List<Notification> Notifications { get; private set; }
    }
}