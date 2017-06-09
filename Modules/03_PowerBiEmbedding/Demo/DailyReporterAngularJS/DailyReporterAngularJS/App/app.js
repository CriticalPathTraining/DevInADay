var myApp;
(function (myApp) {
    var app = angular.module("myApp", ['ngRoute', 'AdalAngular']);
    app.config(function ($locationProvider, $routeProvider, $httpProvider, adalAuthenticationServiceProvider) {
        $locationProvider.html5Mode(true).hashPrefix('!');
        $routeProvider
            .when("/", {
            templateUrl: 'App/views/home.html',
            controller: "homeController",
            controllerAs: "vm"
        })
            .when("/reports", {
            templateUrl: 'App/views/reports.html',
            controller: "reportsController",
            controllerAs: "vm"
        })
            .when("/dashboards", {
            templateUrl: 'App/views/dashboards.html',
            controller: "dashboardsController",
            controllerAs: "vm"
        })
            .otherwise({ redirectTo: "/" });
        var endpoints = {
            "https://graph.windows.net/": "https://graph.windows.net/",
            "https://api.powerbi.com/v1.0/": "https://analysis.windows.net/powerbi/api",
            "https://api.powerbi.com/beta/": "https://analysis.windows.net/powerbi/api"
        };
        adalAuthenticationServiceProvider.init({
            tenant: 'common',
            clientId: '9f23a09a-6961-421e-a75f-a3c49a44ad4d',
            endpoints: endpoints,
            anonymousEndpoints: ['/App/components/topNav.html'],
            extraQueryParameter: 'nux=1',
            cacheLocation: 'localStorage',
            postLogoutRedirectUri: "https://localhost:44300/"
        }, $httpProvider);
    });
})(myApp || (myApp = {}));
//# sourceMappingURL=app.js.map