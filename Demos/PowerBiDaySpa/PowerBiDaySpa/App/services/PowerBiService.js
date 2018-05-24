var myApp;
(function (myApp) {
    var PowerBiService = (function () {
        function PowerBiService() {
        }
        return PowerBiService;
    }());
    PowerBiService.apiRoot = "https://api.powerbi.com/v1.0/myorg/";
    PowerBiService.appWorkspaceId = "dc4eef6d-fbb4-48c6-ad8a-5615b9a9a8cb";
    PowerBiService.appWorkspaceApiRoot = PowerBiService.apiRoot + "groups/" + PowerBiService.appWorkspaceId + "/";
    PowerBiService.GetReports = function () {
        // build URL for reports
        var restUrl = PowerBiService.appWorkspaceApiRoot + "Reports/";
        // execute call against Power BI Service API
        return $.ajax({
            url: restUrl,
            headers: {
                "Accept": "application/json;odata.metadata=minimal;",
                "Authorization": "Bearer " + myApp.SpaAuthService.accessToken
            }
        });
    };
    PowerBiService.GetDashboards = function () {
        // build URL for dashboards
        var restUrl = PowerBiService.appWorkspaceApiRoot + "Dashboards/";
        // execute call against Power BI Service API
        return $.ajax({
            url: restUrl,
            headers: {
                "Accept": "application/json;odata.metadata=minimal;",
                "Authorization": "Bearer " + myApp.SpaAuthService.accessToken
            }
        });
    };
    PowerBiService.GetDatasets = function () {
        // build URL for datasets
        var restUrl = PowerBiService.appWorkspaceApiRoot + "Datasets/";
        // execute call against Power BI Service API
        return $.ajax({
            url: restUrl,
            headers: {
                "Accept": "application/json;odata.metadata=minimal;",
                "Authorization": "Bearer " + myApp.SpaAuthService.accessToken
            }
        });
    };
    myApp.PowerBiService = PowerBiService;
})(myApp || (myApp = {}));
//# sourceMappingURL=powerBiService.js.map