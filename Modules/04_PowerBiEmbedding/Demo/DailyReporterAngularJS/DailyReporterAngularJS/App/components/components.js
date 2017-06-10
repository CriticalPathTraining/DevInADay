var myApp;
(function (myApp) {
    var app = angular.module("myApp");
    var TopNavController = (function () {
        function TopNavController(adalAuthenticationService) {
            this.adalAuthenticationService = adalAuthenticationService;
            console.log("TopNav contructor...");
        }
        ;
        TopNavController.prototype.$onInit = function () {
            console.log("TopNav $onInit...");
            this.adal = this.adalAuthenticationService;
            this.isUserAuthenticated = this.adalAuthenticationService.userInfo.isAuthenticated;
            this.userInfo = this.adalAuthenticationService.userInfo;
            this.login = this.adalAuthenticationService.login;
            this.logout = this.adalAuthenticationService.logOut;
        };
        TopNavController.prototype.loadReport = function (reportId) {
            console.log("Loading report " + reportId);
        };
        return TopNavController;
    }());
    TopNavController.$inject = ['adalAuthenticationService'];
    var TopNav = (function () {
        function TopNav() {
            this.bindings = {};
            this.controller = TopNavController;
            this.templateUrl = '/App/components/topNav.html';
        }
        return TopNav;
    }());
    app.component("topNav", new TopNav());
})(myApp || (myApp = {}));
//# sourceMappingURL=components.js.map