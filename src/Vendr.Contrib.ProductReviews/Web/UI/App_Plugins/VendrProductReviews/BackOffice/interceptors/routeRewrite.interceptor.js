(function () {
    'use strict';

    var routeMap = [
        {
            pattern: /^views\/vendrwishlist\/(.*)-(.*).html(.*)$/gi,
            map: '/app_plugins/vendrwishlist/backoffice/views/$1/$2.html$3'
        },
        {
            pattern: /^views\/vendrwishlist\/(.*).html(.*)$/gi,
            map: '/app_plugins/vendrwishlist/backoffice/views/$1/edit.html$3'
        }
    ];

    function vendrWishListRouteRewritesInterceptor($q) {

        return {
            'request': function (config) {
                
                routeMap.forEach(function (m) {
                    config.url = config.url.replace(m.pattern, m.map);
                });

                return config || $q.when(config);
            }
        };
    }

    angular.module('umbraco.interceptors').factory('vendrWishListRouteRewritesInterceptor', vendrWishListRouteRewritesInterceptor);

}());