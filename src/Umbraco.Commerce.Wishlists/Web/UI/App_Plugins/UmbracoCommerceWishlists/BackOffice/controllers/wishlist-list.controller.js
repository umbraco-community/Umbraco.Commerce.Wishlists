(function () {

    'use strict';

    function wishlistListController($scope, $routeParams, $location, $q, appState, vendrWishlistsResource, navigationService, vendrUtils, vendrRouteCache, vendrLocalStorage) {

        var compositeId = vendrUtils.parseCompositeId($routeParams.id);
        var storeId = compositeId[0];

        var vm = this;

        vm.page = {};
        vm.page.loading = true;

        vm.page.menu = {};
        vm.page.menu.currentSection = appState.getSectionState("currentSection");
        vm.page.menu.currentNode = null;

        vm.page.breadcrumb = {};
        vm.page.breadcrumb.items = [];
        vm.page.breadcrumb.itemClick = function (ancestor) {
            $location.path(ancestor.routePath);
        };

        vm.options = {
            createActions: [],
            filters: [],
            bulkActions: [
                {
                    name: 'Delete',
                    icon: 'icon-trash',
                    doAction: function (bulkItem) {
                        return vendrWishlistsResource.deleteWishlist(bulkItem.id);
                    },
                    getConfirmMessage: function (total) {
                        return $q.resolve("Are you sure you want to delete " + total + " " + (total > 1 ? "items" : "item") + "?");
                    }
                }
            ],
            items: [],
            itemProperties: [
                { alias: 'createDate', header: 'Date', template: "{{ createDate | date : 'MMMM d, yyyy h:mm a' }}" }
            ],
            itemClick: function (itm) {
                $location.path(itm.routePath);
            }
        };

        var hasFilterRouteParams = false;

        vm.options.filters.forEach(fltr => {
            Object.defineProperty(fltr, "value", {
                get: function () {
                    return vendrLocalStorage.get(fltr.localStorageKey) || [];
                },
                set: function (value) {
                    vendrLocalStorage.set(fltr.localStorageKey, value);
                }
            });

            // Initially just check to see if any of the filter are in the route params
            // as if they are, we will reset filters accordingly in a moment, but we
            // need to know if any params exist as we'll wipe out anything that isn't
            // in the querystring
            if ($routeParams[fltr.alias])
                hasFilterRouteParams = true;
        });

        // If we have some filters in the querystring then
        // set the filter values by default, wiping out any
        // cached value they previously had
        if (hasFilterRouteParams) {
            vm.options.filters.forEach(fltr => {
                if ($routeParams[fltr.alias]) {
                    fltr.value = $routeParams[fltr.alias].split(",");
                    $location.search(fltr.alias, null);
                } else {
                    fltr.value = [];
                }
            });
        }

        vm.loadItems = function (opts, callback) {

            if (typeof opts === "function") {
                callback = opts;
                opts = undefined;
            }

            if (!opts) {
                opts = {
                    pageNumber: 1
                };
            }

            // Apply filters
            vm.options.filters.forEach(fltr => {
                if (fltr.value && fltr.value.length > 0) {
                    opts[fltr.alias] = fltr.value;
                } else {
                    delete opts[fltr.alias];
                }
            });

            // Perform search
            vendrWishlistsResource.searchWishlists(storeId, opts).then(function (entities) {
                entities.items.forEach(function (itm) {
                    itm.routePath = '/commerce/vendrwishlists/wishlist-edit/' + vendrUtils.createCompositeId([storeId, itm.id]);
                });
                vm.options.items = entities;
                if (callback) {
                    callback();
                }
            });
        };

        vm.init = function () {

            navigationService.syncTree({ tree: "vendr", path: "-1," + storeId + ",200", forceReload: true }).then(function (syncArgs) {
                vm.page.menu.currentNode = syncArgs.node;
                vm.page.breadcrumb.items = vendrUtils.createBreadcrumbFromTreeNode(syncArgs.node);
                vm.loadItems({
                    pageNumber: 1
                }, function () {
                    vm.page.loading = false;
                });
            });

        };

        vm.init();

        var onVendrWishListEvent = function (evt, args) {
            if (args.entityType === 'Wishlist' && args.storeId === storeId) {
                vm.page.loading = true;
                vm.loadItems({
                    pageNumber: 1
                }, function () {
                    vm.page.loading = false;
                });
            }
        };

        $scope.$on("vendrWishlistDeleted", onVendrWishlistEvent);
    }

    angular.module('umbraco.commerce').controller('Umbraco.Commerce.Wishlist.Controllers.WishlistController', wishlistListController);

}());