using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Vendr.Common.Models;
using Vendr.Contrib.Wishlists.Models;
using Vendr.Contrib.Wishlists.Persistence.Dtos;
using Vendr.Contrib.Wishlists.Persistence.Factories;
using Vendr.Infrastructure;

#if NETFRAMEWORK
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;
#else
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Infrastructure.Persistence.SqlSyntax;
using Umbraco.Extensions;
#endif

namespace Vendr.Contrib.Wishlists.Persistence.Repositories.Implement
{
    internal class WishlistRepository : RepositoryBase, IWishlistRepository
    {
        private readonly IDatabaseUnitOfWork _uow;
        private readonly ISqlContext _sqlSyntax;

        public WishlistRepository(IDatabaseUnitOfWork uow, ISqlContext sqlSyntax)
        {
            _uow = uow;
            _sqlSyntax = sqlSyntax;
        }

        protected Sql<ISqlContext> Sql() => _sqlSyntax.Sql();
        protected ISqlSyntaxProvider SqlSyntax => _sqlSyntax.SqlSyntax;

        public Wishlist GetWishlist(Guid id)
            => GetWishlists(new[] { id }).FirstOrDefault();

        public IEnumerable<Wishlist> GetWishlists(Guid[] ids)
        {
            var sql = Sql()
                .Select("*")
                .From<WishlistDto>()
                .WhereIn<WishlistDto>(x => x.Id, ids);

            var data = _uow.Database.Fetch<WishlistDto>(sql);
            var results = data.Select(EntityFactory.BuildEntity).ToList();

            return results;
        }

        public Wishlist CreateWishlist(string name)
        {
            var wishlist = SaveWishlist(new Wishlist
            {
                Name = name
            });

            return wishlist;
        }

        public PagedResult<Wishlist> SearchWishlists(Guid storeId, string searchTerm = null, string[] customerReferences = null, DateTime? startDate = null, DateTime? endDate = null, long pageNumber = 1, long pageSize = 50)
        {
            customerReferences = customerReferences ?? new string[0];

            var sql = Sql()
                .Select("*")
                .From<WishlistDto>()
                .Where<WishlistDto>(x => x.StoreId == storeId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sql.Where<WishlistDto>(x =>
                    x.Name.Contains(searchTerm)
                );
            }

            if (customerReferences.Length > 0)
            {
                sql.WhereIn<WishlistDto>(x => x.CustomerReference, customerReferences);
            }

            if (startDate != null && startDate >= DateTime.MinValue)
            {
                sql.Where<WishlistDto>(x => x.CreateDate >= startDate.Value);
            }

            if (endDate != null && endDate <= DateTime.MaxValue)
            {
                sql.Where<WishlistDto>(x => x.CreateDate <= endDate.Value);
            }

            sql.OrderByDescending<WishlistDto>(x => x.CreateDate);

            var page = _uow.Database.Page<WishlistDto>(pageNumber, pageSize, sql);

            return new PagedResult<Wishlist>(page.TotalItems, page.CurrentPage, page.ItemsPerPage)
            {
                Items = page.Items.Select(EntityFactory.BuildEntity).ToList()
            };
        }

        public Wishlist SaveWishlist(Wishlist wishlist)
        {
            var dto = EntityFactory.BuildDto(wishlist);

            dto.Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id;
            dto.Name = wishlist.Name;

            _uow.Database.Save(dto);

            return EntityFactory.BuildEntity(dto);
        }

        public void DeleteWishlist(Guid id)
        {
            _uow.Database.Delete<WishlistDto>(id);
        }
    }
}