using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseModelDefinitions;
using Umbraco.Cms.Infrastructure.Persistence.SqlSyntax;

namespace Umbraco.Commerce.Wishlists.Migrations.V_1_0_0
{
    public class CreateWishlistTable : MigrationBase
    {
        public CreateWishlistTable(IMigrationContext context)
            : base(context)
        { }

        protected override void Migrate()
        {
            var wishlistTableName = Constants.DatabaseSchema.Tables.Wishlist;
            var storeTableName = Umbraco.Commerce.Infrastructure.Constants.DatabaseSchema.Tables.Store;

            if (!TableExists(wishlistTableName))
            {
                var nvarcharMaxType = DatabaseType is NPoco.DatabaseTypes.SqlServerCEDatabaseType
                    ? "NTEXT"
                    : "NVARCHAR(MAX)";

                // Create table
                Create.Table(wishlistTableName)
                    .WithColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey($"PK_{wishlistTableName}")
                    .WithColumn("storeId").AsGuid().NotNullable()
                    .WithColumn("orderId").AsGuid().NotNullable()
                    .WithColumn("customerReference").AsString(255).Nullable()
                    .WithColumn("name").AsString(255).Nullable()
                    .WithColumn("createDate").AsDateTime().NotNullable()
                    .WithColumn("updateDate").AsDateTime().NotNullable()
                    .Do();

                // Foreign key constraints
                Create.ForeignKey($"FK_{wishlistTableName}_{storeTableName}")
                    .FromTable(wishlistTableName).ForeignColumn("storeId")
                    .ToTable(storeTableName).PrimaryColumn("id")
                    .Do();
            }
        }
    }
}