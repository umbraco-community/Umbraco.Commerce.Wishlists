using Umbraco.Core.Migrations;
using Vendr.Contrib.Wishlists.Persistence.Dtos;

namespace Vendr.Contrib.Wishlists.Migrations
{
    public class CreateWishlistTable : MigrationBase
    {
        public CreateWishlistTable(IMigrationContext context) : base(context) { }

        public override void Migrate()
        {
            if (TableExists(WishlistDto.TableName)) return;
            Create.Table<WishlistDto>().Do();
        }
    }
}