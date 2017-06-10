var myApp;
(function (myApp) {
    var PowerBiService = (function () {
        function PowerBiService($q, $http, adalAuthenticationService) {
            this.$q = $q;
            this.$http = $http;
            this.adalAuthenticationService = adalAuthenticationService;
            this.apiRoot = "https://api.powerbi.com/v1.0/myOrg/";
        }
        PowerBiService.prototype.GetReports = function () {
            console.log("GetReports...");
            var restUrl = this.apiRoot + "reports/";
            console.log("Calling across network to " + restUrl);
            return this.$http.get(restUrl);
        };
        PowerBiService.prototype.GetReports2 = function () {
            var reports = {
                "value": [
                    {
                        "id": "a8e7b314-2673-4d5f-8ef1-52181f6597cb",
                        "name": "Northwindi Retro",
                        "webUrl": "https://app.powerbi.com/reports/a8e7b314-2673-4d5f-8ef1-52181f6597cb",
                        "embedUrl": "https://app.powerbi.com/reportEmbed?reportId=a8e7b314-2673-4d5f-8ef1-52181f6597cb"
                    },
                    {
                        "id": "77067def-00a2-4a93-80f6-e37eb3b633a4",
                        "name": "Wingtip Sales",
                        "webUrl": "https://app.powerbi.com/reports/77067def-00a2-4a93-80f6-e37eb3b633a4",
                        "embedUrl": "https://app.powerbi.com/reportEmbed?reportId=77067def-00a2-4a93-80f6-e37eb3b633a4"
                    },
                    {
                        "id": "75aac1ff-61b2-443c-bf06-5d6f666ad293",
                        "name": "Wingtip Sales DQ",
                        "webUrl": "https://app.powerbi.com/reports/75aac1ff-61b2-443c-bf06-5d6f666ad293",
                        "embedUrl": "https://app.powerbi.com/reportEmbed?reportId=75aac1ff-61b2-443c-bf06-5d6f666ad293"
                    }
                ]
            };
            return this.$q.when(reports);
        };
        PowerBiService.prototype.GetReport = function (id) {
            var report = {
                "id": "a8e7b314-2673-4d5f-8ef1-52181f6597cb",
                "name": "Northwindi Retro",
                "webUrl": "https://app.powerbi.com/reports/a8e7b314-2673-4d5f-8ef1-52181f6597cb",
                "embedUrl": "https://app.powerbi.com/reportEmbed?reportId=a8e7b314-2673-4d5f-8ef1-52181f6597cb"
            };
            return this.$q.when(report);
        };
        PowerBiService.prototype.getReports = function () {
            console.log("getReports");
            return "Hey Man!!!!";
        };
        PowerBiService.prototype.getDashboards = function () { };
        return PowerBiService;
    }());
    //static $inject: string[] = ["$http", "adalAuthenticationService"];
    //constructor(private $http: ng.IHttpService, private adalAuthenticationService: adal.AdalAuthenticationService) {      }
    PowerBiService.$inject = ["$q", "$http", "adalAuthenticationService"];
    myApp.PowerBiService = PowerBiService;
    angular.module('myApp').service('PowerBi', PowerBiService);
})(myApp || (myApp = {}));
//# sourceMappingURL=services.js.map