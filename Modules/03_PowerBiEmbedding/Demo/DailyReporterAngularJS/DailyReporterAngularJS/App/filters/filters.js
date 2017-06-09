var myApp;
(function (myApp) {
    var AppFilters = (function () {
        function AppFilters() {
        }
        AppFilters.listPriceFilter = function ($filter) {
            return function (price) {
                return "$" + $filter('number')(price, 2) + " USD";
            };
        };
        return AppFilters;
    }());
    AppFilters.$inject = ['$filter'];
    angular.module("myApp").filter("listPrice", AppFilters.listPriceFilter);
})(myApp || (myApp = {}));
//# sourceMappingURL=filters.js.map