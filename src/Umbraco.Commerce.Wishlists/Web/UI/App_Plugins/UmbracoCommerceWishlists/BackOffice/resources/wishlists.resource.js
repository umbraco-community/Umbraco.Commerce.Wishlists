(function() {

    'use strict';

    function commerceWishlistsResource($http, umbRequestHelper) {

        return {

            getWishlist: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/UmbracoCommerceWishList/WishListApi/GetWishList", { params: { id: id } }),
                    "Failed to get wishlist");
            }

        };

    }

    angular.module('umbraco.commerce.resources').factory('commerceWishlistsResource', commerceWishlistsResource);

}());