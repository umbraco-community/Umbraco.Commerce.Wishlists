using Umbraco.Core.Migrations;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Vendr.Contrib.Wishlists.Migrations.V_1_0_0
{
    public class CreateWishlistTable : MigrationBase
    {
        public CreateWishlistTable(IMigrationContext context)
            : base(context)
        { }

        public override void Migrate()
        {
            var wishlistTableName = Constants.DatabaseSchema.Tables.Wishlist;
            var storeTableName = Core.Constants.DatabaseSchema.Tables.Store;

            if (!TableExists(wishlistTableName))
            {
                var nvarcharMaxType = SqlSyntax is SqlCeSyntaxProvider
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