using NPoco;
using System;
using System.Collections.Generic;

#if NETFRAMEWORK
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;
using ConstraintAttribute = Umbraco.Core.Persistence.DatabaseAnnotations.ConstraintAttribute;
#else
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseModelDefinitions;
using ConstraintAttribute = Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations.ConstraintAttribute;
#endif


namespace Vendr.Contrib.Wishlists.Persistence.Dtos
{
    [TableName(TableName)]
    [PrimaryKey("id", AutoIncrement = false)]
    [ExplicitColumns]
    public class WishlistDto
    {
        public const string TableName = Constants.DatabaseSchema.Tables.Wishlist;

        [Column("id")]
        [PrimaryKeyColumn]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Id { get; set; }

        [Column("storeId")]
        public Guid StoreId { get; set; }

        [Column("createDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime CreateDate { get; set; }

        [Column("updateDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime UpdateDate { get; set; }

        [Column("customerReference")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string CustomerReference { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}