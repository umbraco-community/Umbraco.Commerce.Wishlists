(function() {

    'use strict';

    function vendrWishListResource($http, umbRequestHelper) {

        return {

            getWishlist: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrWishList/WishListApi/GetWishList", { params: { id: id } }),
                    "Failed to get wishlist");
            }

        };

    }

    angular.module('vendr.resources').factory('vendrWishListResource', vendrWishListResource);

}());