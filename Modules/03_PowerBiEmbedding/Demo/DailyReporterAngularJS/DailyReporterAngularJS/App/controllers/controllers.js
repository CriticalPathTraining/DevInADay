var myApp;
(function (myApp) {
    var app = angular.module("myApp");
    var HomeController = (function () {
        function HomeController() {
            this.welcomeMessage = "Welcome to the Wingtip Product Manager";
            this.topic1Title = "Add a new product";
            this.topic1Copy = "Click the Add Product link on the navbar aboive to add a new product.";
            this.topic2Title = "See the Product Showcase";
            this.topic2Copy = "Click Product Showcase link in the navbar to see the full set of Wingtip products.";
        }
        return HomeController;
    }());
    HomeController.$inject = [];
    app.controller('homeController', HomeController);
    var ReportsController = (function () {
        function ReportsController(PowerBi) {
            var _this = this;
            this.PowerBi = PowerBi;
            this.loadReport = function (reportId) {
                console.log("LoadReport: " + reportId);
            };
            console.log("ReportsController  constructor");
            console.log("Calling GetReports...");
            if (this.reports === undefined) {
                this.PowerBi.GetReports().then(function (response) {
                    console.log("GetReports Callback...");
                    _this.reports = response.data.value;
                });
            }
        }
        return ReportsController;
    }());
    ReportsController.$inject = ['PowerBi'];
    app.controller('reportsController', ReportsController);
    var DashboardsController = (function () {
        function DashboardsController($location) {
            this.$location = $location;
        }
        return DashboardsController;
    }());
    DashboardsController.$inject = ['$location'];
    app.controller('dashboardsController', DashboardsController);
})(myApp || (myApp = {}));
//# sourceMappingURL=controllers.js.map