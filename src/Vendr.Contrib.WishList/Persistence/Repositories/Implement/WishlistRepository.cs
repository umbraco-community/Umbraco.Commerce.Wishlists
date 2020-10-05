using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Querying;
using Umbraco.Core.Persistence.SqlSyntax;
using Vendr.Contrib.Wishlists.Factories;
using Vendr.Contrib.Wishlists.Models;
using Vendr.Contrib.Wishlists.Persistence.Dtos;
using Vendr.Core;

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

        public Wishlist Get(Guid id)
        {
            var sql = Sql()
                .Select("*")
                .From<WishlistDto>()
                .Where<WishlistDto>(x => x.Id == id);

            var data = _uow.Database.Fetch<WishlistDto>(sql);
            var result = data.Select(WishlistFactory.BuildWishlist).SingleOrDefault();

            return result;
        }

        public IEnumerable<Wishlist> Get(Guid[] ids)
        {
            var sql = Sql()
                .Select("*")
                .From<WishlistDto>()
                .WhereIn<WishlistDto>(x => x.Id, ids);

            var data = _uow.Database.Fetch<WishlistDto>(sql);
            var results = data.Select(WishlistFactory.BuildWishlist).ToList();

            return results;
            //return DoFetchInternal(_uow, "WHERE id IN(@0)", ids);
        }

        public IEnumerable<Wishlist> GetMany(Guid storeId, string productReference, long pageIndex, long pageSize, out long totalRecords)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wishlist> GetForCustomer(Guid storeId, string customerReference, long pageIndex, long pageSize, out long totalRecords, string productReference = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wishlist> GetPagedReviewsByQuery(Guid storeId, IQuery<Wishlist> query, long pageIndex, long pageSize, out long totalRecords)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wishlist> SearchWishLists(Guid storeId, long pageIndex, long pageSize, out long totalRecords, string[] statuses, decimal[] ratings, string searchTerm = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }

        public Wishlist Save(Wishlist wishlist)
        {
            throw new NotImplementedException();
        }

        public Wishlist Insert(Wishlist wishlist)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}