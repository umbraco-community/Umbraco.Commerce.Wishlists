﻿using Vendr.Contrib.Wishlists.Models;
using Vendr.Contrib.Wishlists.Persistence.Dtos;
using Vendr.Extensions;

namespace Vendr.Contrib.Wishlists.Persistence.Factories
{
    internal static class EntityFactory
    {
        public static Wishlist BuildEntity(WishlistDto dto)
        {
            dto.MustNotBeNull(nameof(dto));

            var wishlist = new Wishlist(dto.Id, dto.OrderId)
            {
                StoreId = dto.StoreId,
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate,
                Name = dto.Name,
                CustomerReference = dto.CustomerReference
            };

            return wishlist;
        }

        public static WishlistDto BuildDto(Wishlist wishlist)
        {
            wishlist.MustNotBeNull(nameof(wishlist));

            var dto = new WishlistDto
            {
                Id = wishlist.Id,
                StoreId = wishlist.StoreId,
                CreateDate = wishlist.CreateDate,
                UpdateDate = wishlist.UpdateDate,
                Name = wishlist.Name,
                CustomerReference = wishlist.CustomerReference
            };

            return dto;
        }
    }
}